//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx\\str_lncollmast.srs"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("str_lncollmast",Sybase.PowerBuilder.PBGroupType.Structure,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx",null,"pbservice125")]
internal class c__str_lncollmast : Sybase.PowerBuilder.PBStructure
{
	public Sybase.PowerBuilder.PBString member_no = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_memdet = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_collmastlist = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString collmast_no = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_collmastdet = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_collmemco = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_mrtg1 = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_mrtg2 = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_mrtg3 = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_review = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_prop = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_colluse = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString entry_id = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString branch_id = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString coop_id = Sybase.PowerBuilder.PBString.DefaultValue;

	public override object Clone()
	{
		c__str_lncollmast vv = (c__str_lncollmast)base.Clone();
 		vv.member_no = member_no;
		vv.xml_memdet = xml_memdet;
		vv.xml_collmastlist = xml_collmastlist;
		vv.collmast_no = collmast_no;
		vv.xml_collmastdet = xml_collmastdet;
		vv.xml_collmemco = xml_collmemco;
		vv.xml_mrtg1 = xml_mrtg1;
		vv.xml_mrtg2 = xml_mrtg2;
		vv.xml_mrtg3 = xml_mrtg3;
		vv.xml_review = xml_review;
		vv.xml_prop = xml_prop;
		vv.xml_colluse = xml_colluse;
		vv.entry_id = entry_id;
		vv.branch_id = branch_id;
		vv.coop_id = coop_id;
		return vv;
	}

	public static explicit operator c__str_lncollmast(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedArray)
		{
			Sybase.PowerBuilder.PBUnboundedArray a = (Sybase.PowerBuilder.PBUnboundedArray)v;
			c__str_lncollmast vv = new c__str_lncollmast();
			vv.member_no = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[1]);
			vv.xml_memdet = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[2]);
			vv.xml_collmastlist = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[3]);
			vv.collmast_no = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[4]);
			vv.xml_collmastdet = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[5]);
			vv.xml_collmemco = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[6]);
			vv.xml_mrtg1 = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[7]);
			vv.xml_mrtg2 = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[8]);
			vv.xml_mrtg3 = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[9]);
			vv.xml_review = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[10]);
			vv.xml_prop = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[11]);
			vv.xml_colluse = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[12]);
			vv.entry_id = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[13]);
			vv.branch_id = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[14]);
			vv.coop_id = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[15]);

			return vv;
		}
		else
		{
			return (c__str_lncollmast) v.Value;
		}
	}

	public override Sybase.PowerBuilder.PBUnboundedAnyArray ToUnboundedAnyArray()
	{
		Sybase.PowerBuilder.PBUnboundedAnyArray a = new Sybase.PowerBuilder.PBUnboundedAnyArray();
		a.Add(new Sybase.PowerBuilder.PBAny(this.member_no));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_memdet));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_collmastlist));
		a.Add(new Sybase.PowerBuilder.PBAny(this.collmast_no));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_collmastdet));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_collmemco));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_mrtg1));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_mrtg2));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_mrtg3));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_review));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_prop));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_colluse));
		a.Add(new Sybase.PowerBuilder.PBAny(this.entry_id));
		a.Add(new Sybase.PowerBuilder.PBAny(this.branch_id));
		a.Add(new Sybase.PowerBuilder.PBAny(this.coop_id));

		return a;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_lncollmast")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Ansi )]
internal struct c__str_lncollmast_ansi
{
	public string member_no;
	public string xml_memdet;
	public string xml_collmastlist;
	public string collmast_no;
	public string xml_collmastdet;
	public string xml_collmemco;
	public string xml_mrtg1;
	public string xml_mrtg2;
	public string xml_mrtg3;
	public string xml_review;
	public string xml_prop;
	public string xml_colluse;
	public string entry_id;
	public string branch_id;
	public string coop_id;

	public static implicit operator c__str_lncollmast_ansi(c__str_lncollmast other)
	{

		c__str_lncollmast_ansi ret = new c__str_lncollmast_ansi();

		ret.member_no = other.member_no;

		ret.xml_memdet = other.xml_memdet;

		ret.xml_collmastlist = other.xml_collmastlist;

		ret.collmast_no = other.collmast_no;

		ret.xml_collmastdet = other.xml_collmastdet;

		ret.xml_collmemco = other.xml_collmemco;

		ret.xml_mrtg1 = other.xml_mrtg1;

		ret.xml_mrtg2 = other.xml_mrtg2;

		ret.xml_mrtg3 = other.xml_mrtg3;

		ret.xml_review = other.xml_review;

		ret.xml_prop = other.xml_prop;

		ret.xml_colluse = other.xml_colluse;

		ret.entry_id = other.entry_id;

		ret.branch_id = other.branch_id;

		ret.coop_id = other.coop_id;

		return ret;
	}

	public static implicit operator c__str_lncollmast(c__str_lncollmast_ansi other)
	{

		c__str_lncollmast ret = new c__str_lncollmast();

		ret.member_no = other.member_no;

		ret.xml_memdet = other.xml_memdet;

		ret.xml_collmastlist = other.xml_collmastlist;

		ret.collmast_no = other.collmast_no;

		ret.xml_collmastdet = other.xml_collmastdet;

		ret.xml_collmemco = other.xml_collmemco;

		ret.xml_mrtg1 = other.xml_mrtg1;

		ret.xml_mrtg2 = other.xml_mrtg2;

		ret.xml_mrtg3 = other.xml_mrtg3;

		ret.xml_review = other.xml_review;

		ret.xml_prop = other.xml_prop;

		ret.xml_colluse = other.xml_colluse;

		ret.entry_id = other.entry_id;

		ret.branch_id = other.branch_id;

		ret.coop_id = other.coop_id;

		return ret;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_lncollmast")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Unicode )]
internal struct c__str_lncollmast_unicode
{
	public string member_no;
	public string xml_memdet;
	public string xml_collmastlist;
	public string collmast_no;
	public string xml_collmastdet;
	public string xml_collmemco;
	public string xml_mrtg1;
	public string xml_mrtg2;
	public string xml_mrtg3;
	public string xml_review;
	public string xml_prop;
	public string xml_colluse;
	public string entry_id;
	public string branch_id;
	public string coop_id;

	public static implicit operator c__str_lncollmast_unicode(c__str_lncollmast other)
	{

		c__str_lncollmast_unicode ret = new c__str_lncollmast_unicode();

		ret.member_no = other.member_no;

		ret.xml_memdet = other.xml_memdet;

		ret.xml_collmastlist = other.xml_collmastlist;

		ret.collmast_no = other.collmast_no;

		ret.xml_collmastdet = other.xml_collmastdet;

		ret.xml_collmemco = other.xml_collmemco;

		ret.xml_mrtg1 = other.xml_mrtg1;

		ret.xml_mrtg2 = other.xml_mrtg2;

		ret.xml_mrtg3 = other.xml_mrtg3;

		ret.xml_review = other.xml_review;

		ret.xml_prop = other.xml_prop;

		ret.xml_colluse = other.xml_colluse;

		ret.entry_id = other.entry_id;

		ret.branch_id = other.branch_id;

		ret.coop_id = other.coop_id;

		return ret;
	}

	public static implicit operator c__str_lncollmast(c__str_lncollmast_unicode other)
	{

		c__str_lncollmast ret = new c__str_lncollmast();

		ret.member_no = other.member_no;

		ret.xml_memdet = other.xml_memdet;

		ret.xml_collmastlist = other.xml_collmastlist;

		ret.collmast_no = other.collmast_no;

		ret.xml_collmastdet = other.xml_collmastdet;

		ret.xml_collmemco = other.xml_collmemco;

		ret.xml_mrtg1 = other.xml_mrtg1;

		ret.xml_mrtg2 = other.xml_mrtg2;

		ret.xml_mrtg3 = other.xml_mrtg3;

		ret.xml_review = other.xml_review;

		ret.xml_prop = other.xml_prop;

		ret.xml_colluse = other.xml_colluse;

		ret.entry_id = other.entry_id;

		ret.branch_id = other.branch_id;

		ret.coop_id = other.coop_id;

		return ret;
	}
}
 