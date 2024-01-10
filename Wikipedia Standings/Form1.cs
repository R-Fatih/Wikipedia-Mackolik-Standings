using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wikipedia_Standings
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		public class Root
		{
			public int id { get; set; }
			public List<List<object>> s { get; set; }
			public List<List<object>> f { get; set; }
			public List<List<object>> r { get; set; }
			public List<List<object>> d { get; set; }
		}
		//0 pozisyon
		//1 takım adı
		//2 iç saha oynanan
		//3 deplasman oynanan
		//4 iç saha Galibiyet
		//5 deplasman Galibiyet
		//6 iç saha beraberlik
		//7 deplasman beraberlik
		//8 iç saha mağlubiyet
		//9 deplasman mağlubiyet
		//10 içerde attığı
		//11 dışarda attığı
		//12 içerde yediği
		//13 içerde yediği
		//14 iç saha puan
		//15 deplasman puan
		//16 bilinmeyen 0
		//17 ceza puanı
		string mainstring = "|team{0} ={1} |win_{1}= {2} |draw_{1}= {3} |loss_{1}= {4} |gf_{1}={5} |ga_{1}= {6} |name_{1} = [[{7}]] |status_{1}=";
		StringBuilder sb = new StringBuilder();
		// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

		private void button1_Click(object sender, EventArgs e)
		{//1. lig //63975
		 //süper lig //63860
			HttpClient http = new HttpClient();
			string myJsonResponse = http.GetStringAsync("https://arsiv.mackolik.com/AjaxHandlers/StandingHandler.ashx?op=standing&id=59419").Result;
			//string teams = http.GetStringAsync("https://raw.githubusercontent.com/R-Fatih/Wikipedia-Football/main/teams.json").Result;
			 Team[] teams = http.GetFromJsonAsync<Team[]>("https://raw.githubusercontent.com/R-Fatih/Wikipedia-Football/main/teams.json").Result;

			Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
			for (int i = 0; i < myDeserializedClass.s.Count; i++)
			{
				richTextBox2.AppendText(myDeserializedClass.s[i][0] + "," + myDeserializedClass.s[i][1]+"\n");
						richTextBox1.AppendText(	string.Format(mainstring, (i+1), myDeserializedClass.s[i][0], Convert.ToInt32(myDeserializedClass.s[i][4]) + Convert.ToInt32(myDeserializedClass.s[i][5]), Convert.ToInt32(myDeserializedClass.s[i][6]) + Convert.ToInt32(myDeserializedClass.s[i][7]), Convert.ToInt32(myDeserializedClass.s[i][8]) + Convert.ToInt32(myDeserializedClass.s[i][9]), Convert.ToInt32(myDeserializedClass.s[i][10]) + Convert.ToInt32(myDeserializedClass.s[i][11]), Convert.ToInt32(myDeserializedClass.s[i][12]) + Convert.ToInt32(myDeserializedClass.s[i][13]), myDeserializedClass.s[i][1]) +"\n");
						//richTextBox1.AppendText(	string.Format(mainstring, (i+1), teams.Where(a=>a.MaçkolikId== myDeserializedClass.s[i][0].ToString()).ToList()[0].KısaKodu, Convert.ToInt32(myDeserializedClass.s[i][4]) + Convert.ToInt32(myDeserializedClass.s[i][5]), Convert.ToInt32(myDeserializedClass.s[i][6]) + Convert.ToInt32(myDeserializedClass.s[i][7]), Convert.ToInt32(myDeserializedClass.s[i][8]) + Convert.ToInt32(myDeserializedClass.s[i][9]), Convert.ToInt32(myDeserializedClass.s[i][10]) + Convert.ToInt32(myDeserializedClass.s[i][11]), Convert.ToInt32(myDeserializedClass.s[i][12]) + Convert.ToInt32(myDeserializedClass.s[i][13]), teams.Where(a => a.MaçkolikId == myDeserializedClass.s[i][0].ToString()).ToList()[0].TakımAdı) +"\n");

				if (Convert.ToInt32(myDeserializedClass.s[i][17].ToString()) != 0)
					sb.Append($"|adjust_points_{myDeserializedClass.s[i][1].ToString()}={myDeserializedClass.s[i][17].ToString()}");
			}
			//richTextBox1.AppendText(sb.ToString());
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
