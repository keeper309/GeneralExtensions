using System;

namespace GameCore.GeneralExtensions
{
    public class SafeAction : SafeActionBase<Action, int>
    {
        protected override void Call(Action action, int _)
        {
            action();
        }

        public void Invoke()
        {
            if (IsEmpty)
                return;

            InvokeInner(0);
        }
    }

    public class SafeAction<T1>
        : SafeActionBase<Action<T1>, T1>
    {
        protected override void Call(Action<T1> action, T1 t1)
        {
            action(t1);
        }

        public void Invoke(T1 t1)
        {
            if (IsEmpty)
                return;

            InvokeInner(t1);
        }
    }

    public class SafeAction<T1, T2>
        : SafeActionBase<Action<T1, T2>, SafeAction<T1, T2>.ArgsTuple<T1, T2>>
    {
        protected override void Call(Action<T1, T2> action, ArgsTuple<T1, T2> args)
        {
            action(args.t1, args.t2);
        }

        public void Invoke(T1 t1, T2 t2)
        {
            if (IsEmpty)
                return;

            InvokeInner(new ArgsTuple<T1, T2>(t1, t2));
        }

        public struct ArgsTuple<T1, T2>
        {
            public readonly T1 t1;
            public readonly T2 t2;

            public ArgsTuple(T1 t1, T2 t2)
            {
                this.t1 = t1;
                this.t2 = t2;
            }
        }
    }

    public class SafeAction<T1, T2, T3>
        : SafeActionBase<Action<T1, T2, T3>, SafeAction<T1, T2, T3>.ArgsTuple<T1, T2, T3>>
    {
        protected override void Call(Action<T1, T2, T3> action, ArgsTuple<T1, T2, T3> args)
        {
            action(args.t1, args.t2, args.t3);
        }

        public void Invoke(T1 t1, T2 t2, T3 t3)
        {
            if (IsEmpty)
                return;

            InvokeInner(new ArgsTuple<T1, T2, T3>(t1, t2, t3));
        }

        public struct ArgsTuple<T1, T2, T3>
        {
            public readonly T1 t1;
            public readonly T2 t2;
            public readonly T3 t3;

            public ArgsTuple(T1 t1, T2 t2, T3 t3)
            {
                this.t1 = t1;
                this.t2 = t2;
                this.t3 = t3;
            }
        }
    }

    public class SafeAction<T1, T2, T3, T4>
        : SafeActionBase<Action<T1, T2, T3, T4>, SafeAction<T1, T2, T3, T4>.ArgsTuple<T1, T2, T3, T4>>
    {
        protected override void Call(Action<T1, T2, T3, T4> action, ArgsTuple<T1, T2, T3, T4> args)
        {
            action(args.t1, args.t2, args.t3, args.t4);
        }

        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4)
        {
            if (IsEmpty)
                return;

            InvokeInner(new ArgsTuple<T1, T2, T3, T4>(t1, t2, t3, t4));
        }

        public struct ArgsTuple<T1, T2, T3, T4>
        {
            public readonly T1 t1;
            public readonly T2 t2;
            public readonly T3 t3;
            public readonly T4 t4;

            public ArgsTuple(T1 t1, T2 t2, T3 t3, T4 t4)
            {
                this.t1 = t1;
                this.t2 = t2;
                this.t3 = t3;
                this.t4 = t4;
            }
        }
    }
}
