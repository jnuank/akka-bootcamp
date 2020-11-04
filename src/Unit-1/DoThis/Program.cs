﻿using System;
﻿using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // initialize MyActorSystem
            // YOU NEED TO FILL IN HERE
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "_consoleWriterActor");
            
            Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, "validationActor");
            
            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");
            
            // tell console reader to begin
            //YOU NEED TO FILL IN HERE
            // consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);
            validationActor.Tell("start");
            
            // blocks the main thread from exiting until the actor system is shut down
            // ブロックされるまで、consoleReaderActorをTellし続ける…？
            MyActorSystem.WhenTerminated.Wait();
        }
    }
    #endregion
}
