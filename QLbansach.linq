<Query Kind="Statements">
  <Connection>
    <ID>48e68481-ad98-4c27-a240-8adc6b1137de</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-GQSGD1K\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <Database>QLBanSach</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

CHU_DEs
	.Select(c => new {
        Mcd = c.Mcd,
        TenChuDe = c.Ten_chu_de,
    })
    .Dump();
----------------------------------------
CHU_DEs
    .Select(c => new {
        Mcd = c.Mcd,
        TenChuDe = c.Ten_chu_de,
        SoLuongSach = c.SACHes.Count()
    })
    .Dump();
---------------------------------------
CHU_DEs
    .Where(c => c.SACHes.Any())
    .Select(c => new {
        Mcd = c.Mcd,
        TenChuDe = c.Ten_chu_de
    })
    .Dump();
------------------------------------------
SACHes
    .Where(s => s.Mcd == 5)
    .Select(s => new {
        MaSach = s.Ms,
        TenSach = s.Ten_sach,
        GiaBan = s.Don_gia
    })
    .Dump();
---------------------------------------------
SACHes
    .OrderByDescending(s => s.Ngay_cap_nhat)
    .Take(5)
    .Select(s => new {
        MaSach = s.Ms,
        TenSach = s.Ten_sach,
        AnhBia = s.Hinh_minh_hoa
    })
    .Dump();
--------------------------------------------
CT_DAT_HANGs
    .GroupBy(ct => new { ct.Ms, ct.SACH.Ten_sach, ct.SACH.Hinh_minh_hoa })
    .Select(g => new {
        MaSach = g.Key.Ms,
        TenSach = g.Key.Ten_sach,
        AnhBia = g.Key.Hinh_minh_hoa,
        TongSoLuongBan = g.Sum(x => x.So_luong)
    })
    .OrderByDescending(x => x.TongSoLuongBan)
    .Take(5)
    .Dump();
----------------------------------------------
QUANG_CAOs
    .Where(qc => qc.Ngay_het_han >= new DateTime(2020, 09, 15))
    .Select(qc => new {
        TieuDe = qc.TenCTy,
        Link = qc.HREF,
        NgayHetHan = qc.Ngay_het_han
    })
    .Dump();
-----------------------------------------------
THAM_GIAs
    .Where(tg => tg.Ms == 2)
    .Select(tg => new {
        MaTacGia = tg.Mtg,
        TenTacGia = tg.TAC_GIA.Ten_tac_gia,
        VaiTro = tg.Vai_tro
    })
    .Dump();
-------------------------------------------------
DON_DAT_HANGs
    .Where(dh => dh.Da_giao_hang == true)
    .Select(dh => new {
        MaDonHang = dh.Sdh,
        MaClient = dh.Mkh,
        NgayDat = dh.Ngay_dat_hang,
        NgayGiao = dh.Ngay_giao_hang
    })
    .Dump();