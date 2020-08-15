using System;
using System.Collections.Generic;
using System.Text;

namespace ParserTask
{
    //make all buisness logic 
    class BuisnessLogic
    {
        string url = @"https://api.stackexchange.com/2.2/search?order=desc&sort=activity&intitle=beautiful&site=stackoverflow";
        private void getJson()
        {
            ParseData parseData = new ParseData();
            parseData.DownloadFile(url);
            parseData.Decompress("json.gz", "json.json");
            parseData.readJson("json.json");
        }
        public BuisnessLogic()
        {
            getJson();
        }
    }
}
