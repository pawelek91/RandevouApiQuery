using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RandevouApiCommunication
{
    internal static class ApiQueriesIOCcontainer
    {
        static readonly Dictionary<Type, Type> Container = new Dictionary<Type, Type>();

        public static void Register<TContract, TImplementation>() where TImplementation: class, TContract
        {
            ConstructorInfo constructor = typeof(TImplementation).GetConstructors()[0];
            if (constructor.GetParameters().Length != 0)
                throw ContainerException;

            Container[typeof(TContract)] = typeof(TImplementation);
        }

        public static Type Get<TContract>()
            => Container[typeof(TContract)];


        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private static Object Resolve(Type tContract)
        {
            Type impl = Container[tContract];
            ConstructorInfo constructor = impl.GetConstructors()[0];
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
            {
                return Activator.CreateInstance(impl);
            }
            else
                throw ContainerException;
        }

        private static InvalidOperationException ContainerException
            =>  new  InvalidOperationException("Communication container provides only classess with public non-parameters constructor");
    }
}
