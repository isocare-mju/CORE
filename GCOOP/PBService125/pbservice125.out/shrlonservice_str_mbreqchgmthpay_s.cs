//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\shrlonservice.pbl\\shrlonservice.pblx\\str_mbreqchgmthpay.srs"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("str_mbreqchgmthpay",Sybase.PowerBuilder.PBGroupType.Structure,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\shrlonservice.pbl\\shrlonservice.pblx",null,"pbservice125")]
internal class c__str_mbreqchgmthpay : Sybase.PowerBuilder.PBStructure
{
	public Sybase.PowerBuilder.PBString coop_id = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString member_no = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString memcoop_id = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBDateTime chgmthpay_date = Sybase.PowerBuilder.PBDateTime.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_master = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_detailshr = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_detaillon = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString entry_id = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_reqopt = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_reqlist = Sybase.PowerBuilder.PBString.DefaultValue;

	public override object Clone()
	{
		c__str_mbreqchgmthpay vv = (c__str_mbreqchgmthpay)base.Clone();
 		vv.coop_id = coop_id;
		vv.member_no = member_no;
		vv.memcoop_id = memcoop_id;
		vv.chgmthpay_date = chgmthpay_date;
		vv.xml_master = xml_master;
		vv.xml_detailshr = xml_detailshr;
		vv.xml_detaillon = xml_detaillon;
		vv.entry_id = entry_id;
		vv.xml_reqopt = xml_reqopt;
		vv.xml_reqlist = xml_reqlist;
		return vv;
	}

	public static explicit operator c__str_mbreqchgmthpay(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedArray)
		{
			Sybase.PowerBuilder.PBUnboundedArray a = (Sybase.PowerBuilder.PBUnboundedArray)v;
			c__str_mbreqchgmthpay vv = new c__str_mbreqchgmthpay();
			vv.coop_id = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[1]);
			vv.member_no = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[2]);
			vv.memcoop_id = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[3]);
			vv.chgmthpay_date = (Sybase.PowerBuilder.PBDateTime)((Sybase.PowerBuilder.PBAny)a[4]);
			vv.xml_master = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[5]);
			vv.xml_detailshr = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[6]);
			vv.xml_detaillon = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[7]);
			vv.entry_id = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[8]);
			vv.xml_reqopt = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[9]);
			vv.xml_reqlist = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[10]);

			return vv;
		}
		else
		{
			return (c__str_mbreqchgmthpay) v.Value;
		}
	}

	public override Sybase.PowerBuilder.PBUnboundedAnyArray ToUnboundedAnyArray()
	{
		Sybase.PowerBuilder.PBUnboundedAnyArray a = new Sybase.PowerBuilder.PBUnboundedAnyArray();
		a.Add(new Sybase.PowerBuilder.PBAny(this.coop_id));
		a.Add(new Sybase.PowerBuilder.PBAny(this.member_no));
		a.Add(new Sybase.PowerBuilder.PBAny(this.memcoop_id));
		a.Add(new Sybase.PowerBuilder.PBAny(this.chgmthpay_date));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_master));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_detailshr));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_detaillon));
		a.Add(new Sybase.PowerBuilder.PBAny(this.entry_id));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_reqopt));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_reqlist));

		return a;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_mbreqchgmthpay")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Ansi )]
internal struct c__str_mbreqchgmthpay_ansi
{
	public string coop_id;
	public string member_no;
	public string memcoop_id;
	public System.DateTime chgmthpay_date;
	public string xml_master;
	public string xml_detailshr;
	public string xml_detaillon;
	public string entry_id;
	public string xml_reqopt;
	public string xml_reqlist;

	public static implicit operator c__str_mbreqchgmthpay_ansi(c__str_mbreqchgmthpay other)
	{

		c__str_mbreqchgmthpay_ansi ret = new c__str_mbreqchgmthpay_ansi();

		ret.coop_id = other.coop_id;

		ret.member_no = other.member_no;

		ret.memcoop_id = other.memcoop_id;

		ret.chgmthpay_date = other.chgmthpay_date;

		ret.xml_master = other.xml_master;

		ret.xml_detailshr = other.xml_detailshr;

		ret.xml_detaillon = other.xml_detaillon;

		ret.entry_id = other.entry_id;

		ret.xml_reqopt = other.xml_reqopt;

		ret.xml_reqlist = other.xml_reqlist;

		return ret;
	}

	public static implicit operator c__str_mbreqchgmthpay(c__str_mbreqchgmthpay_ansi other)
	{

		c__str_mbreqchgmthpay ret = new c__str_mbreqchgmthpay();

		ret.coop_id = other.coop_id;

		ret.member_no = other.member_no;

		ret.memcoop_id = other.memcoop_id;

		ret.chgmthpay_date = other.chgmthpay_date;

		ret.xml_master = other.xml_master;

		ret.xml_detailshr = other.xml_detailshr;

		ret.xml_detaillon = other.xml_detaillon;

		ret.entry_id = other.entry_id;

		ret.xml_reqopt = other.xml_reqopt;

		ret.xml_reqlist = other.xml_reqlist;

		return ret;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_mbreqchgmthpay")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Unicode )]
internal struct c__str_mbreqchgmthpay_unicode
{
	public string coop_id;
	public string member_no;
	public string memcoop_id;
	public System.DateTime chgmthpay_date;
	public string xml_master;
	public string xml_detailshr;
	public string xml_detaillon;
	public string entry_id;
	public string xml_reqopt;
	public string xml_reqlist;

	public static implicit operator c__str_mbreqchgmthpay_unicode(c__str_mbreqchgmthpay other)
	{

		c__str_mbreqchgmthpay_unicode ret = new c__str_mbreqchgmthpay_unicode();

		ret.coop_id = other.coop_id;

		ret.member_no = other.member_no;

		ret.memcoop_id = other.memcoop_id;

		ret.chgmthpay_date = other.chgmthpay_date;

		ret.xml_master = other.xml_master;

		ret.xml_detailshr = other.xml_detailshr;

		ret.xml_detaillon = other.xml_detaillon;

		ret.entry_id = other.entry_id;

		ret.xml_reqopt = other.xml_reqopt;

		ret.xml_reqlist = other.xml_reqlist;

		return ret;
	}

	public static implicit operator c__str_mbreqchgmthpay(c__str_mbreqchgmthpay_unicode other)
	{

		c__str_mbreqchgmthpay ret = new c__str_mbreqchgmthpay();

		ret.coop_id = other.coop_id;

		ret.member_no = other.member_no;

		ret.memcoop_id = other.memcoop_id;

		ret.chgmthpay_date = other.chgmthpay_date;

		ret.xml_master = other.xml_master;

		ret.xml_detailshr = other.xml_detailshr;

		ret.xml_detaillon = other.xml_detaillon;

		ret.entry_id = other.entry_id;

		ret.xml_reqopt = other.xml_reqopt;

		ret.xml_reqlist = other.xml_reqlist;

		return ret;
	}
}
 