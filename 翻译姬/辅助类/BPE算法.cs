using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 翻译姬;
public class BPE算法 {

    private CoreBPE _corePBE;

    private Dictionary<string, int> special_tokens = new Dictionary<string, int>{
                                { "<|endoftext|>", 100257},
                                { "<|fim_prefix|>", 100258},
                                { "<|fim_middle|>", 100259},
                                { "<|fim_suffix|>", 100260},
                                { "<|endofprompt|>", 100276}
                            };

    /// <summary>
    /// https://openaipublic.blob.core.windows.net/encodings/cl100k_base.tiktoken
    /// 传入cl100k_base.tiktoken的UTF8文本行
    /// </summary>
    /// <param name="模型词表">GPT模型词表</param>
    /// <exception cref="FormatException"></exception>
    /*public BPE算法() {
        //格式化
        var bpeDict = new Dictionary<byte[], int>(new ByteArrayComparer());
        if (!File.Exists(Program.GPT词表路径)) {
            throw new Exception("模型词表路径不存在，请联系开发者");
        }
        string[] 模型词表;
        try {
            模型词表 = File.ReadAllLines(Program.GPT词表路径);
        } catch (Exception ex) {
            throw new Exception($"模型词表读取错误：{ex.Message}");
        }
        foreach (var str in 模型词表) {
            string line = str.Trim();
            if (string.IsNullOrWhiteSpace(line)) {
                continue;
            }

            var tokens = line.Split(' ');
            if (tokens.Length != 2) {
                throw new FormatException($"内容格式化失败");
            }

            var tokenBytes = Convert.FromBase64String(tokens[0]);
            var rank = int.Parse(tokens[1]);
            bpeDict[tokenBytes] = rank;
        }
        _corePBE = new CoreBPE(bpeDict, special_tokens, @"(?i:'s|'t|'re|'ve|'m|'ll|'d)|[^\r\n\p{L}\p{N}]?\p{L}+|\p{N}{1,3}| ?[^\s\p{L}\p{N}]+[\r\n]*|\s*[\r\n]+|\s+(?!\S)|\s+");
    }*/

    public int Token计算(string text) {
        try {
            return Encode(text).Count;
        } catch (Exception ex) {
            throw new Exception("GPT Token计算失败，请前往GPT设置下载选择模型词表");
        }
    }

    public List<int> Encode(string text, object allowedSpecial = null, object disallowedSpecial = null) {
        if (allowedSpecial == null) {
            allowedSpecial = new HashSet<string>();
        }
        if (disallowedSpecial == null) {
            disallowedSpecial = "all";
        }

        var allowedSpecialSet = allowedSpecial.Equals("all") ? SpecialTokensSet() : new HashSet<string>((IEnumerable<string>)allowedSpecial);
        var disallowedSpecialSet = disallowedSpecial.Equals("all") ? new HashSet<string>(SpecialTokensSet().Except(allowedSpecialSet)) : new HashSet<string>((IEnumerable<string>)disallowedSpecial);

        if (disallowedSpecialSet.Count() > 0) {
            var specialTokenRegex = SpecialTokenRegex(disallowedSpecialSet);
            var match = specialTokenRegex.Match(text);
            if (match.Success) {
                throw new Exception(match.Value);
            }
        }

        return _corePBE.EncodeNative(text, allowedSpecialSet).Item1;
    }

    public string Decode(List<int> tokens) {
        var ret = _corePBE.DecodeNative(tokens.ToArray());
        string str = Encoding.UTF8.GetString(ret.ToArray());
        return str;
    }

    private HashSet<string> SpecialTokensSet() {
        return new HashSet<string>(special_tokens.Keys);
    }

    private Regex SpecialTokenRegex(HashSet<string> tokens) {
        var inner = string.Join("|", tokens.Select(Regex.Escape));
        return new Regex($"({inner})");
    }

}
public class EncodingSettingModel {
    public string Name { get; set; }

    /// <summary>
    /// regex
    /// </summary>
    public string PatStr { get; set; }


    public int? ExplicitNVocab { get; set; }

    /// <summary>
    /// tiktoken file
    /// </summary>
    public Dictionary<byte[], int> MergeableRanks { get; set; }

    public Dictionary<string, int> SpecialTokens { get; set; }


    public int MaxTokenValue {
        get {
            return Math.Max(MergeableRanks.Values.Max(), SpecialTokens.Values.Max());
        }
    }


}
public class CoreBPE {
    private Dictionary<string, int> _specialTokensEncoder { get; set; }

    // TODO private max_token_value ??
    private Dictionary<byte[], int> _encoder { get; set; }

    private Regex _specialRegex { get; set; }

    private Regex _regex { get; set; }


    private Lazy<Dictionary<int, byte[]>> _lazyDecoder;

    private Dictionary<int, byte[]> Decoder => _lazyDecoder.Value;


    private Dictionary<int, string> _specialTokensDecoder { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="encoder"></param>
    /// <param name="specialTokensEncoder"></param>
    /// <param name="pattern"></param>
    public CoreBPE(Dictionary<byte[], int> encoder, Dictionary<string, int> specialTokensEncoder, string pattern) {
        _encoder = encoder;
        _regex = new Regex(pattern, RegexOptions.Compiled);
        _specialRegex = new Regex(string.Join("|", specialTokensEncoder.Keys.Select(s => Regex.Escape(s))), RegexOptions.Compiled);
        _specialTokensEncoder = specialTokensEncoder;

        _lazyDecoder = new Lazy<Dictionary<int, byte[]>>(() => {
            var decoder = _encoder.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

            if (_encoder.Count != decoder.Count) {
                throw new ArgumentException("Encoder and decoder sizes don't match");
            }

            return decoder;
        });

        _specialTokensDecoder = specialTokensEncoder.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        var sortedTokenBytes = _encoder.Keys.ToList();
    }

    public (List<int>, int) EncodeNative(string text, HashSet<string> allowedSpecial) {
        Regex specialRegex = _specialRegex;
        Regex regex = _regex;
        var ret = new List<int>();

        int start = 0;
        int lastPieceTokenLen = 0;
        while (true) {
            Match nextSpecial;
            int startFind = start;
            while (true) {
                nextSpecial = specialRegex.Match(text, startFind);
                if (!nextSpecial.Success) break;
                if (allowedSpecial.Contains(text.Substring(nextSpecial.Index, nextSpecial.Length))) break;
                startFind = nextSpecial.Index + 1;
            }
            int end = nextSpecial.Success ? nextSpecial.Index : text.Length;

            foreach (Match mat in regex.Matches(text.Substring(start, end - start))) {
                var piece = Encoding.UTF8.GetBytes(mat.Value);
                if (_encoder.TryGetValue(piece, out int token)) {
                    lastPieceTokenLen = 1;
                    ret.Add(token);
                    continue;
                }
                var tokens = BytePairEncoding.BytePairEncode(piece, _encoder);
                lastPieceTokenLen = tokens.Count;
                ret.AddRange(tokens);
            }

            if (nextSpecial.Success) {
                var piece = nextSpecial.Value;
                var token = _specialTokensEncoder[piece];
                ret.Add(token);
                start = nextSpecial.Index + nextSpecial.Length;
                lastPieceTokenLen = 0;
            } else {
                break;
            }
        }

        return (ret, lastPieceTokenLen);
    }

    public byte[] DecodeNative(int[] tokens) {
        var ret = new List<byte>(tokens.Length * 2);
        foreach (var token in tokens) {
            byte[] tokenBytes = { };
            if (Decoder.TryGetValue(token, out var value)) {
                tokenBytes = value;
            } else {
                if (_specialTokensDecoder.TryGetValue(token, out var valueS)) {
                    tokenBytes = UTF8Encoding.UTF8.GetBytes(valueS);
                }
            }

            if (tokenBytes.Length > 0) {
                ret.AddRange(tokenBytes);
            }
        }
        return ret.ToArray();
    }
}
public class BytePairEncoding {
    static List<T> BytePairMerge<T>(byte[] piece, Dictionary<byte[], int> ranks, Func<(int, int), T> f) {
        var parts = Enumerable.Range(0, piece.Length + 1).Select(i => (i, int.MaxValue)).ToList();
        int? GetRank(int startIdx, int skip = 0) {
            if (startIdx + skip + 2 < parts.Count) {
                var slice = 数组分割(piece, parts[startIdx].Item1, parts[startIdx + skip + 2].Item1);
                if (ranks.TryGetValue(slice, out var rank)) {
                    return rank;
                }
            }
            return null;
        }
        for (int i = 0; i < parts.Count - 2; i++) {
            var rank = GetRank(i);
            if (rank != null) {
                Debug.Assert(rank.Value != int.MaxValue);
                parts[i] = (parts[i].Item1, rank.Value);
            }
        }
        while (parts.Count > 1) {
            var minRank = (int.MaxValue, 0);
            for (int i = 0; i < parts.Count - 1; i++) {
                if (parts[i].Item2 < minRank.Item1) {
                    minRank = (parts[i].Item2, i);
                }
            }
            if (minRank.Item1 != int.MaxValue) {
                int i = minRank.Item2;
                parts[i] = (parts[i].Item1, GetRank(i, 1) ?? int.MaxValue);
                if (i > 0) {
                    parts[i - 1] = (parts[i - 1].Item1, GetRank(i - 1, 1) ?? int.MaxValue);
                }
                parts.RemoveAt(i + 1);
            } else {
                break;
            }
        }
        var outList = new List<T>(parts.Count - 1);
        for (int i = 0; i < parts.Count - 1; i++) {
            outList.Add(f((parts[i].Item1, parts[i + 1].Item1)));
        }
        return outList;
    }
    //framwork不支持列表模式，linq模拟
    public static T[] 数组分割<T>(T[] buf, int start, int end) {
        return buf.Skip(start).Take(end - start).ToArray();
    }

    public static List<int> BytePairEncode(byte[] piece, Dictionary<byte[], int> ranks) {
        if (piece.Length == 1) {
            return new List<int> { ranks[piece] };
        }
        return BytePairMerge(piece, ranks, p => ranks[数组分割(piece, p.Item1, p.Item2)]);
    }
}
public class ByteArrayComparer : IEqualityComparer<byte[]> {
    public bool Equals(byte[] x, byte[] y) {
        if (x == null || y == null) {
            return x == y;
        }
        if (x.Length != y.Length) {
            return false;
        }
        for (int i = 0; i < x.Length; i++) {
            if (x[i] != y[i]) {
                return false;
            }
        }
        return true;
    }

    public int GetHashCode(byte[] obj) {
        if (obj == null) {
            throw new ArgumentNullException(nameof(obj));
        }
        int hash = 17;
        foreach (byte b in obj) {
            hash = hash * 31 + b;
        }
        return hash;
    }
}