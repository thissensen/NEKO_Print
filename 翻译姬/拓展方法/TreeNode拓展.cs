using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬 {
    public static class TreeNode拓展 {
        /// <summary>
        /// 当前大节点一共有多少个最小子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int 获取最终子节点数(this TreeNode node) {
            Stack<TreeNode> s = new Stack<TreeNode>();
            s.Push(node);
            int num = 0;
            while (s.Count > 0) {
                TreeNode t = s.Pop();
                if (t.Nodes.Count > 0) {
                    foreach (TreeNode n in t.Nodes) {
                        s.Push(n);
                    }
                } else {
                    num++;
                }
            }
            return num;
        }

    }
}
