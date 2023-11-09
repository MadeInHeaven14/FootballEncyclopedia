using FootballEncyclopedia.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEncyclopedia.Services
{
    internal class DBConnection
    {
        public static EncyclopediaEntities connection = new EncyclopediaEntities();
    }
}
