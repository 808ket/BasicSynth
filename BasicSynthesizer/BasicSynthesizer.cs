using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicSynthesizer
{
    public partial class BasicSynthesizer : Form
    {//jede sekunde wollen wir 44100 samples generieren
        private const int SAMPLE_RATE = 44100;
       // Die maximale grösse eines samples darf 16 Bit betragen
        private const short BITS_PER_SAMPLE = 16;
        public BasicSynthesizer()
        {
            InitializeComponent();
        }



        private void BasicSynthesizer_KeyDown(object sender, KeyEventArgs e)
        {
            IEnumerable<Oscillator> oscillators = this.Controls.OfType<Oscillator>().Where(o => o.On);
        
            Random random = new Random();
            // in diesem Array werden die Samples gespeichert nachdem sie generiert wurden
            // da wir 16Bit nutzen ist unser Array short
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            //temporary variable, hier entscheiden wir welche frequenz gewählt wird anhand des gedrückten Keys
            float frequency;
            int oscillatorsCount = oscillators.Count();
            switch (e.KeyCode)
            {
                case Keys.Y:
                    frequency = 65.4f;          //C2
                    break;
                case Keys.X:
                    frequency = 138.59f;          //C3
                    break;
                case Keys.C:
                    frequency = 261.62f;          //C4
                    break;
                case Keys.V:
                    frequency = 523.25f;          //C5
                    break;
                case Keys.B:
                    frequency = 1046.5f;          //C6
                    break;
                case Keys.N:
                    frequency = 65.4f;          //C7
                    break;
                case Keys.M:
                    frequency = 65.4f;          //C8
                    break;
                default:
                    return;

            }
            
            foreach (Oscillator oscillator in this.Controls.OfType<Oscillator>())
            {
                int samplesPerWaveLength = (int)(SAMPLE_RATE / frequency);
                short ampStep = (short)((short.MaxValue * 2) / samplesPerWaveLength);
                short tempSample;
                switch (oscillator.WaveForm)
                    {
                    case WaveForm.Sine:
                        //wir füllen diese Werte mit Samples
                        for (int i = 0; i < SAMPLE_RATE; i++)
                        {//an jeder der 44100 positionen in wave konvertieren wir diesen algorthmus in einen char
                            wave[i] += Convert.ToInt16((short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)) / oscillatorsCount);
                        }
                        break;
                     case WaveForm.Square:
                for (int i = 0; i < SAMPLE_RATE; i++)
                {//Gleichung zur Berechnung einer Sinuswelle
                    wave[i] += Convert.ToInt16((short.MaxValue * Math.Sign(Math.Sin((Math.PI * 2 * frequency) / SAMPLE_RATE * i))) / oscillatorsCount);
                }
            break;
            case WaveForm.Saw:
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                tempSample = -short.MaxValue;
                for (int j = 0; j < samplesPerWaveLength && i < SAMPLE_RATE; j++)
                {
                    tempSample += ampStep;
                    wave[i++] += Convert.ToInt16(tempSample / oscillatorsCount);
                }
                i--;
            }
            break; 
            case WaveForm.Triangle:
                tempSample = -short.MaxValue;
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                if (Math.Abs(tempSample + ampStep) > short.MaxValue)
                {
                    ampStep = (short)-ampStep;
                }
                tempSample += ampStep;
                wave[i] += Convert.ToInt16(tempSample / oscillatorsCount);
            }
            break;
            case WaveForm.Noise:
                for (int i = 0; i < SAMPLE_RATE;i++)
            {
                wave[i] += Convert.ToInt16(random.Next(-short.MaxValue, short.MaxValue) / oscillatorsCount);
            }
            break;
        }
    }

            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
            //contain the information we want for the binary writer
            using (MemoryStream memoryStream = new MemoryStream())
                //to write the stream  
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                short blockAlign = BITS_PER_SAMPLE / 8;
                int subChunckTwoSize = SAMPLE_RATE * blockAlign;
                //Der Wert 0x52494646 wird in den Stream geschrieben
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binaryWriter.Write(36 + subChunckTwoSize);
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                binaryWriter.Write(16);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write(SAMPLE_RATE);
                binaryWriter.Write(SAMPLE_RATE * blockAlign);
                binaryWriter.Write(BITS_PER_SAMPLE);
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' }) ;
                binaryWriter.Write(subChunckTwoSize);
                binaryWriter.Write(binaryWave);
                memoryStream.Position = 0;
                new SoundPlayer(memoryStream).Play();


            }
        }

        private void BasicSynthesizer_Load(object sender, EventArgs e)
        {

        }

        private void oscillator1_Enter(object sender, EventArgs e)
        {

        }
    }

    public enum WaveForm
    {
        Sine, Square, Saw, Triangle, Noise
    }
}
