using Microsoft.VisualStudio.TestTools.UnitTesting;
using Singletons;
using System;

namespace UnitTests
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void RegisterAndGet()
        {
            SingletonManager.Clear();

            var player = new Player("Dave", 35);
            SingletonManager.Register(player);

            var singleton = SingletonManager.Get<Player>();

            Assert.AreEqual(player, singleton);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterMultipleException()
        {
            SingletonManager.Clear();

            var player = new Player("Dave", 35);
            SingletonManager.Register(player);
            SingletonManager.Register(player);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNoneException()
        {
            SingletonManager.Clear();

            SingletonManager.Get<Player>();
        }

        [TestMethod]
        public void RegisterOrReplace()
        {
            SingletonManager.Clear();

            SingletonManager.RegisterOrReplace(new Player("Dave", 35));
            var a = SingletonManager.Get<Player>();
            SingletonManager.RegisterOrReplace(new Player("James", 24));
            var b = SingletonManager.Get<Player>();

            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        public void TryGet()
        {
            SingletonManager.Clear();

            bool success1 = SingletonManager.TryGet<Player>(out var throwaway);

            SingletonManager.Register(new Player("Dave", 35));
            bool success2 = SingletonManager.TryGet<Player>(out var player);

            Assert.AreEqual(false, success1);
            Assert.AreEqual(true, success2);
        }

        [TestMethod]
        public void Unregister()
        {
            SingletonManager.Clear();

            SingletonManager.Register(new Player("Dave", 35));
            SingletonManager.Unregister<Player>();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveException()
        {
            SingletonManager.Clear();

            SingletonManager.Unregister<Player>();
        }

        [TestMethod]
        public void HasBeenRegistered()
        {
            SingletonManager.Clear();

            bool doesntExist = SingletonManager.HasBeenRegistered<Player>();

            SingletonManager.Register(new Player("Dave", 35));
            bool doesExist = SingletonManager.HasBeenRegistered<Player>();

            Assert.AreEqual(false, doesntExist);
            Assert.AreEqual(true, doesExist);
        }

        public class Player
        {
            public string Name { get; }
            public int Age { get; }

            public Player(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }
    }
}
