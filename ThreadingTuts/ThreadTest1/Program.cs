
using System.Data;
using System.Threading;

namespace Program;

public class Program
{
    static Mutex mutex = new Mutex();

    static List<int> m_list = new List<int>();

    static void Worker()
    {
        
        for (int i = 0; i < 10; i++)
        {
            int t = Random.Shared.Next(0, 100);
            Console.WriteLine("{0} | {1}\t\t{2}", Thread.CurrentThread.Name, i, t);
         
            Thread.Sleep(100 + t);
        }
    }

    static void WorkerM()
    {
        m_list = new List<int>();

        int to = 10;
        int inc = 0;

        while (inc < to)
        {
            lock (mutex)
            {
                int t = Random.Shared.Next(0, 100);
                // Console.WriteLine("{0} | {1}\t\t{2}", Thread.CurrentThread.Name, inc, t);
                Thread.Sleep(100 + t);
                m_list.Add(inc);
                inc++;
            }
            
        }
    }



    public static void Main(string[] args)
    {
        // Start one thread
        // Thread single = new Thread(new ThreadStart(Worker))
        // {
        //     Name = "Thread (single)"
        // };
        

        // single.Start();
        // single.Join();
        // Console.WriteLine(single.Name + " Finished\n");


        // Thread a  = new Thread(new ThreadStart(Worker))
        // {
        //     Name = "Thread (a)   "
        // };

        // Thread b  = new Thread(new ThreadStart(Worker))
        // {
        //     Name = "Thread (b)"
        // };

        // a.Start();
        // b.Start();

        // a.Join();
        // b.Join();

        // Console.WriteLine("Finished\n");



        Thread m_a  = new Thread(new ThreadStart(WorkerM))
        {
            Name = "Thread (a)   "
        };

        Thread m_b  = new Thread(new ThreadStart(WorkerM))
        {
            Name = "Thread (b)"
        };

        m_a.Start();
        m_b.Start();

        // m_a.Join();
        // m_b.Join();

        while (m_a.IsAlive && m_b.IsAlive)
        {
            Thread.Sleep(100);
            Console.Write(".");
        }
        
        // Console.WriteLine("Finished\n");

        foreach (int a in m_list)
        {
            Console.Write("{0} ", a);
        }


    }
}