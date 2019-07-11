using Alea_Belli.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alea_Belli.Core.Network
{
    /// <summary>
    /// The interface which exposes game data to say the rendering system
    /// </summary>
    public interface IAleaBelliGame
    {
        List<Regiment> AllRegiments { get; }

        void AddRegiment(Regiment r);
    }

    /// <summary>
    /// The simplest game type - no networking or threading
    /// </summary>
    public class SinglePlayerAleaBelliGame : AleaBelliGame
    {

    }

    /// <summary>
    /// The basic game data
    /// </summary>
    public abstract class AleaBelliGame : IAleaBelliGame
    {
        private Dictionary<int, Regiment> regiments = new Dictionary<int, Regiment>();

        internal Dictionary<int, Regiment> Regiments
        {
            get
            {
                return regiments;
            }
        }

        public void AddRegiment(Regiment r)
        {
            regiments[r.RegimentId] = r;
        }


        public List<Regiment> AllRegiments
        {
            get
            {
                return regiments.Values.ToList<Regiment>();
            }
        }
    }
}
