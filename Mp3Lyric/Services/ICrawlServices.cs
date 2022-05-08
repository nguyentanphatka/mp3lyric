using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Lyric.Services
{
    public interface ICrawlServices
    {
        Task CrawlLyricAsync();
    }
}
