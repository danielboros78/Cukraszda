using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cukraszda
{
  public partial class Form1 : Form
  {
    List<Cukraszda> sutik = new List<Cukraszda>();
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
      Beolvasas();
      MasodikFeladat();
      DijnyertesSzamolas();
      MaximumKivalasztas();
      MinimumKivalasztas();
      ListaKiiratas();
      TipusVizsgalat();
    }

    private void TipusVizsgalat()
    {
      Dictionary<string, int> stat = new Dictionary<string, int>();
      for (int i = 0; i < sutik.Count; i++)
      {
        if (!stat.ContainsKey(sutik[i].Tipus))
        {
          stat.Add(sutik[i].Tipus, 1);
        }
        else
        {
          stat[sutik[i].Tipus]++;
        }
      }
      StreamWriter ki = new StreamWriter("stat.csv");
      foreach (var s in stat)
      {
        ki.WriteLine($"{s.Key};{s.Value}");
      }
      ki.Close();
    }

    private void ListaKiiratas()
    {
      List<string> sutinevek = new List<string>();
      StreamWriter ki = new StreamWriter("lista.txt");
      for (int i = 0; i < sutik.Count; i++)
      {
        if (!sutinevek.Contains(sutik[i].Nev))
        {
          ki.WriteLine($"{sutik[i].Nev} {sutik[i].Tipus}");
          sutinevek.Add(sutik[i].Nev);
        }
      }
      ki.Close();
    }

    private void DijnyertesSzamolas()
    {
      int dijnyertesek = 0;
      for (int i = 0; i < sutik.Count; i++)
      {
        if (sutik[i].Dijazott == true)
        {
          dijnyertesek++;
        }
      }
      tbDijnyertes.Text = $"{dijnyertesek} féle díjnyertes édességből választhat";
    }

    private void MinimumKivalasztas()
    {
      int index = 0;
      int min = int.MaxValue;
      for (int i = 0; i < sutik.Count; i++)
      {
        if (min > sutik[i].Ar)
        {
          min = sutik[i].Ar;
          index = i;
        }
      }
      tbLegolcsobb.Text = sutik[index].Nev;
      tbLegolcsobbInfo.Text = $"{sutik[index].Ar}/{sutik[index].Ertekesites}";
    }

    private void MaximumKivalasztas()
    {
      int index = 0;
      int max = 0;
      for (int i = 0; i < sutik.Count; i++)
      {
        if (max < sutik[i].Ar)
        {
          max = sutik[i].Ar;
          index = i;
        }
      }
      tbLegdragabb.Text = sutik[index].Nev;
      tbLegdragabbInfo.Text = $"{sutik[index].Ar}/{sutik[index].Ertekesites}";
    }

    private void MasodikFeladat()
    {
      int index = RandomGeneralas();
      tbAjanlott.Text = $"Mai ajánlatunk: {sutik[index].Nev}";
    }

    private int RandomGeneralas()
    {
      Random rnd = new Random(Guid.NewGuid().GetHashCode());
      return rnd.Next(0, sutik.Count);
    }

    private void Beolvasas()
    {
      StreamReader be = new StreamReader("cuki.txt");
      while (!be.EndOfStream)
      {
        string[] a = be.ReadLine().Split(';');
        sutik.Add(new Cukraszda(a[0], a[1], Convert.ToBoolean(a[2]), Convert.ToInt32(a[3]), a[4]));
      }
      be.Close();
    }

    private void btnAjanlatMentese_Click(object sender, EventArgs e)
    {
      //bool van = false;
      //for (int i = 0; i < sutik.Count; i++)
      //{
      //  if (sutik[i].Tipus == tbSutiTipus.Text)
      //  {
      //    van = true;
      //  }
      //}
      //if (tbSutiTipus.Text == "")
      //{
      //  MessageBox.Show("Nem írtál be süteménynevet!");
      //  van = true;
      //}
      //if (!van)
      //{
      //  MessageBox.Show("Nincs megfelelő sütink! Kérjük, válassz mást!");
      //}
      if (tbSutiTipus.Text == "")
      {
        MessageBox.Show("Nem írtál be típust!");
      }
      else
      {
        List<Cukraszda> ajanlat = new List<Cukraszda>();
        foreach (var s in sutik)
        {
          if (s.Tipus == tbSutiTipus.Text)
          {
            ajanlat.Add(s);
          }
        }
        if (ajanlat.Count == 0)
        {
          MessageBox.Show("Válassz mást!");
        }
        else
        {
          try
          {
            double atlagar = 0;
            StreamWriter ki = new StreamWriter("ajanlat.txt");
            foreach (var a in ajanlat)
            {
              ki.WriteLine(a.ToString());
              atlagar += a.Ar;
            }
            MessageBox.Show($"{tbSutiTipus.Text} típusú sütemények átlagára:\n{atlagar /= ajanlat.Count:N2}.- Ft");
            ki.Close();
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message);
          }
        }
      }
    }

    private void btnFelvitel_Click(object sender, EventArgs e)
    {
      if (tbSuti.Text == "" || tbTipus.Text == "" || tbAr.Text == "" || tbEgyseg.Text == "")
      {
        MessageBox.Show("Nem adtál meg minden adatot!");
      }
      else
      {
        int ar;
        if (int.TryParse(tbAr.Text, out ar))
        {
          Cukraszda uj = new Cukraszda(tbSuti.Text, tbTipus.Text, cbDijazott.Checked, ar, tbEgyseg.Text);
          sutik.Add(uj);
          StreamWriter ki = new StreamWriter("cuki.txt",true);
          ki.WriteLine($"{uj.Nev};{uj.Tipus};{uj.Dijazott};{uj.Ar};{uj.Ertekesites}");
          ki.Close();
        }
        else
        {
          MessageBox.Show("Az új sütemény ára nem jó!");
          tbAr.Focus();
        }
      }
    }
  }
}
