
using System;
using System.IO.Ports;
using System.IO;
using System.Text;

class PirateOBDSimulator4KKL
{
    static SerialPort serialPort;
    static byte[] fakeFlash = new byte[256 * 1024]; // 256KB finta flash

    static void Main()
    {
        Console.WriteLine("PirateOBDSimulator4KKL - Simulatore EDC15C5 via K-Line (KKL)");

        Console.Write("Inserisci la porta COM (es. COM5): ");
        string comPort = Console.ReadLine();

        serialPort = new SerialPort(comPort, 10400, Parity.None, 8, StopBits.One);
        serialPort.DataReceived += OnDataReceived;

        try
        {
            serialPort.Open();
            Console.WriteLine("Simulatore attivo su " + comPort);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore apertura porta: " + ex.Message);
            return;
        }

        // Riempimento Flash fittizia con dati casuali
        Random rnd = new Random();
        rnd.NextBytes(fakeFlash);

        Console.WriteLine("Premi un tasto per terminare...");
        Console.ReadKey();
        serialPort.Close();
    }

    static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        int length = serialPort.BytesToRead;
        byte[] buffer = new byte[length];
        serialPort.Read(buffer, 0, length);

        Console.WriteLine("Ricevuto: " + BitConverter.ToString(buffer));

        if (Match(buffer, "68-6A-F1-01-00"))
        {
            serialPort.Write(HexToBytes("48-6B-11-C1-00"), 0, 5);
        }
        else if (Match(buffer, "68-6A-F1-03-00-00-00"))
        {
            serialPort.Write(HexToBytes("48-6B-11-43-01-01-00-00"), 0, 8);
        }
        else if (Match(buffer, "68-6A-F1-04-00"))
        {
            serialPort.Write(HexToBytes("48-6B-11-44-00"), 0, 5);
        }
        else if (Match(buffer, "68-6A-F1-1A-80"))
        {
            serialPort.Write(HexToBytes("48-6B-11-5A-80-45-44-43"), 0, 7); // "EDC"
        }
        else if (Match(buffer, "68-6A-F1-23-A0-00"))
        {
            SaveFlashToFile();
            serialPort.Write(HexToBytes("48-6B-11-63-01"), 0, 5); // risposta positiva semplificata
        }
    }

    static bool Match(byte[] buffer, string hexPattern)
    {
        var expected = HexToBytes(hexPattern);
        if (buffer.Length != expected.Length) return false;
        for (int i = 0; i < buffer.Length; i++)
            if (buffer[i] != expected[i]) return false;
        return true;
    }

    static byte[] HexToBytes(string hex)
    {
        string[] hexValues = hex.Split('-');
        byte[] bytes = new byte[hexValues.Length];
        for (int i = 0; i < hexValues.Length; i++)
            bytes[i] = Convert.ToByte(hexValues[i], 16);
        return bytes;
    }

    static void SaveFlashToFile()
    {
        string path = "EDC15C5_FlashDump.bin";
        File.WriteAllBytes(path, fakeFlash);
        Console.WriteLine("Flash ECU salvata in: " + path);
    }
}
