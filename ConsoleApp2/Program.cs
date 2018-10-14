using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;


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

            FileStream neuerDatenstrom = new FileStream("test.txt", FileMode.Create, FileAccess.Write);
            //neuerDatenstrom.WriteByte((byte)42);
            neuerDatenstrom.WriteByte((byte)1);
            BinaryWriter writer = new BinaryWriter(neuerDatenstrom);
            //writer.Write(new byte[] { 66, 106, 111, 101, 114, 110 });
            writer.Close();
            writer = null;
            neuerDatenstrom = null;


            //FileStream nochnNeuerDatenstrom = new FileStream("test.txt", FileMode.Open, FileAccess.Read);
            //BinaryReader reader = new BinaryReader(nochnNeuerDatenstrom);
            //byte[] all = reader.ReadBytes((int)nochnNeuerDatenstrom.Length);
            //for (int i = 0; i < all.Length; i++)
            //{
            //    Console.WriteLine(all[i]);
            //}
            //reader.Close();
            //Console.Read();


            neuerDatenstrom = new FileStream("test.txt", FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(neuerDatenstrom);
            byte[] all = reader.ReadBytes((int)neuerDatenstrom.Length);
            BitArray bitall = new BitArray(all);
            reader.Close();
            Console.WriteLine(bitall);
            Console.Read();


            //neuerDatenstrom = new FileStream("test.txt", FileMode.Open, FileAccess.Read);
            //bool[] ich = new bool[neuerDatenstrom.Length * 8];
            //BitReader aktBit = new BitReader(neuerDatenstrom);

            //bool? hilf = false;

            //for (int i = 0; hilf != null & i < ich.Length; i++)
            //{
            //    hilf = aktBit.ReadBit(true);

            //    if (hilf == true) ich[i] = (bool)hilf;
            //    if (hilf == false) ich[i] = (bool)hilf;
            //}

            //for (int i = 1; i <= ich.Length; i++)
            //{
            //    if (ich[i - 1])
            //    {
            //        Console.Write("1");
            //    }
            //    else
            //    {
            //        Console.Write("0");
            //    }
            //    if (i % 8 == 0) Console.WriteLine();
            //}

            //Console.Read();


        }
    }
}

