using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬 {
    class 线程池 {
        private List<Task> 线程队列 = new List<Task>();
        private int 执行中线程数;
        private int 最大线程数;
        private int index;//取值下标
        public 线程池(int 最大线程数) {//创建固定数目线程池
            this.最大线程数 = 最大线程数;
            执行中线程数 = 1;
            index = 0;
        }
        //添加线程
        public void 添加线程(Task newTask) {
            线程队列.Add(newTask);
        }
        //执行线程并等待完成
        public void 执行并等待() {
            while (index < 线程队列.Count) {
                if (执行中线程数 <= 最大线程数) {
                    Task t = 线程队列[index];//取出任务
                    Task.WhenAll(t).ContinueWith((s) => {
                        执行中线程数--;//执行完就--
                    });
                    t.Start();//执行
                    执行中线程数++;//计数+1
                    index++;
                } else {
                    //任务队列塞满了
                    continue;
                }
            }
            //等待全部执行完成
            foreach (Task t in 线程队列) {
                t.Wait();
            }
        }
    }
}
