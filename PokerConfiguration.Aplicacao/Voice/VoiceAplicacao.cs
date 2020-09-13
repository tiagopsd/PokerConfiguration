using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using PokerConfiguration.Dominio;

namespace PokerConfiguration
{
    public class VoiceAplicacao
    {
        protected SpeechSynthesizer SpeakerVoz { get; set; }

        static bool AudioAtivo = true;

        private static object _lock = new object();

        private static readonly List<Thread> _threadsVoice = new List<Thread>();


        static Queue<string> _falas = new Queue<string>();

        static string ultimaVoz = "";

        public void CarregarVoice()
        {
            if (SpeakerVoz == null)
                SpeakerVoz = new SpeechSynthesizer();

            SpeakerVoz.Rate = 2;

            var z = "";
            foreach (var item in IdiomasDisponiveis().Where(item => item.Contains("pt-BR")))
            {
                z = item;
                return;
            }

            SpeakerVoz.SelectVoice(z);
        }


        public IEnumerable<string> IdiomasDisponiveis()
        {
            return SpeakerVoz.GetInstalledVoices().Select(voice => voice.VoiceInfo.Culture.ToString()).ToList();
        }

        public bool SintetizarVoz(string Texto)
        {
            lock (_lock)
            {
                if (AudioAtivo)
                {
                    if (SpeakerVoz == null)
                        CarregarVoice();


                    SpeakerVoz.Speak(Texto);

                    ultimaVoz = Texto;
                    return true;
                }
                return false;
            }
        }

        public void AtivaAudio()
        {
            LimpaFila();
            AudioAtivo = true;
        }

        public void CancelaAtivaAudio()
        {
            if (SpeakerVoz == null)
                CarregarVoice();

            if (AudioAtivo)
            {
                SpeakerVoz.Pause();
                AudioAtivo = false;
            }
            else if (!AudioAtivo)
            {
                LimpaFila();
                SpeakerVoz.Resume();
                AudioAtivo = true;
            }
        }

        public void Pausar()
        {
            SpeakerVoz.Pause();
        }

        public void Continuar()
        {
            SpeakerVoz.Resume();
        }

        public void LimpaFila()
        {
            _falas.Clear();
        }

        public string ObterMensagem(TipoMensagemEnum tipoMensagem)
        {
            return VoiceFactory.ObterMensagem(tipoMensagem);
        }

        public void AdicionaFala(string text)
        {
            LimpaFila();
            _falas.Enqueue(text);
        }

        private void processaFila()
        {
            if (!_falas.Any()) return;
            string texto = _falas.Dequeue();
            {
                if (!string.IsNullOrWhiteSpace(texto))
                    SintetizarVoz(texto);

            }
        }
    }
}
