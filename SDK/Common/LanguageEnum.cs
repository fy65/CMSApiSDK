using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fy.SDK
{
    public enum LanguageEnum
    {
        [DescriptionAttribute("cn")]
        中文 = 1,
        [DescriptionAttribute("en")]
        英文 = 2,
        [DescriptionAttribute("ft")]
        繁体 = 3,
        [DescriptionAttribute("fa")]
        法文 = 4,
        [DescriptionAttribute("ja")]
        日文 = 5,

        [DescriptionAttribute("ru")]
        俄文 = 6,
        [DescriptionAttribute("ko")]
        韩文 = 7,

    }
}
