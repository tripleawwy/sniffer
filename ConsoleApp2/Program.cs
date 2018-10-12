using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class BitReader
    {
        int _bit = 8;
        byte _currentByte;
        Stream _stream;
        public BitReader(Stream stream)
        { _stream = stream; }

        /** Endian encoding requiered 
         *  bigEndian = true
         *  littleEndian = false
         */

        public bool? ReadBit(bool inputEndian)
        {
            if (_bit == 8)
            {

                var r = _stream.ReadByte();
                if (r == -1) return null;
                _bit = 0;
                _currentByte = (byte)r;
            }
            bool value;
            if (!inputEndian)
                value = (_currentByte & (1 << _bit)) > 0;
            else
                value = (_currentByte & (1 << (7 - _bit))) > 0;

            _bit++;
            return value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //FileStream neuerDatenstrom = new FileStream("test1.txt", FileMode.Create, FileAccess.Write);
            //BinaryWriter writer = new BinaryWriter(neuerDatenstrom);
            //writer.Write('b');
            //writer.Write('j');
            //writer.Write('o');
            //writer.Write('e');
            //writer.Write('r');
            //writer.Write('n');
            //writer.Close();
            //writer = null;
            //neuerDatenstrom = null;

            FileStream neuerDatenstrom = new FileStream("test.txt", FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(neuerDatenstrom);
            char[] all = reader.ReadChars((int)neuerDatenstrom.Length);
            reader.Close();
            Console.WriteLine(all);
            //Console.Read();


            neuerDatenstrom = new FileStream("test.txt", FileMode.Open, FileAccess.Read);
            bool[] ich = new bool[neuerDatenstrom.Length * 8];
            BitReader aktBit = new BitReader(neuerDatenstrom);

            bool? hilf = false;

            for (int i = 0; hilf != null & i < ich.Length; i++)
            {
                hilf = aktBit.ReadBit(true);

                if (hilf == true) ich[i] = (bool)hilf;
                if (hilf == false) ich[i] = (bool)hilf;
            }

            for (int i = 1; i <= ich.Length; i++)
            {
                if (ich[i - 1])
                {
                    Console.Write("1");
                }
                else
                {
                    Console.Write("0");
                }
                if (i % 8 == 0) Console.WriteLine();
            }

            int arsch = 42;

            Console.WriteLine("Arsch = " + arsch);
            Console.WriteLine("Arsch = " + arsch++);
            Console.WriteLine("Arsch = " + ++arsch);
            Console.Read();

            //FileStream achDuMeineGuete = new FileStream("test2.txt.", FileMode.Create, FileAccess.Write);
            //BinaryWriter writer = new BinaryWriter(achDuMeineGuete);


        }
    }
}

