using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukraszda
{
  class Cukraszda
  {
    private string nev;

    public string Nev
    {
      get { return nev; }
    }

    private string tipus;

    public string Tipus
    {
      get { return tipus; }
    }

    private bool dijazott;

    public bool Dijazott
    {
      get { return dijazott; }
    }

    private int ar;

    public int Ar
    {
      get { return ar; }
    }

    private string ertekesites;

    public string Ertekesites
    {
      get { return ertekesites; }
    }

    public Cukraszda(string nev, string tipus, bool dijazott, int ar, string ertekesites)
    {
      this.nev = nev;
      this.tipus = tipus;
      this.dijazott = dijazott;
      this.ar = ar;
      this.ertekesites = ertekesites;
    }
  }
}
