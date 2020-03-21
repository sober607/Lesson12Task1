using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace Lesson12task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Главный поток метода Main, "+ Thread.CurrentThread.ManagedThreadId);
            Func<string> funcStream = new Func<string>(SomeMethod);
            funcStream.BeginInvoke(CallbackMethod, null);

            Console.ReadLine();
        }

        public static string SomeMethod ()
        {
            Console.WriteLine("Начало выполнение метода SomeMethod");
            Console.WriteLine("Поток метода: Id {0}", Thread.CurrentThread.ManagedThreadId);
            DateTime date1 = DateTime.Now;
            Thread.Sleep(3000);
            return Convert.ToString(date1);
        }

        public static void CallbackMethod(IAsyncResult asyncResult)
        {
            Console.WriteLine("Начало работы CallbackMethod");
            AsyncResult asyncResultVariable = asyncResult as AsyncResult;
            Func<string> caller = (Func<string>)asyncResultVariable.AsyncDelegate;
            string dateInfo = caller.EndInvoke(asyncResultVariable);
            //string resultOfOperation = string.Format()
            Console.WriteLine("Результат метода SomeMethod, это время и дата: " + dateInfo + $", ID потока {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
