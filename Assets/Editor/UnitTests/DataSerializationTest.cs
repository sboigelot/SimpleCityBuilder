using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Assets.Scripts.Models;
using Assets.Scripts.Serialization;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTests
{
    public class DataSerializationTest
    {
        [Test]
        public void SerializeDateTime()
        {
            var data = new GameDate { Year = 2005, Week = 15 };

            var ms = new MemoryStream();
            DataSerializer.Instance.Serialize(ms, data);
            ms.Flush();

            ms.Position = 0;
            var sr = new StreamReader(ms);
            string xml = sr.ReadToEnd();
            Console.WriteLine(xml);

            ms.Position = 0;
            var data2 = DataSerializer.Instance.DeSerialize<GameDate>(ms);
            Assert.AreEqual(data.Year, data2.Year);
            Assert.AreEqual(data.Week, data2.Week);
        }

        public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}