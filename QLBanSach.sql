SELECT Mcd, Ten_chu_de from CHU_DE
------------------------------
SELECT 
    cd.Mcd, 
    cd.Ten_chu_de, 
    COUNT(s.Ms) AS SoLuongSach
FROM 
    CHU_DE cd
LEFT JOIN 
    Sach s ON cd.Mcd = s.Mcd
GROUP BY 
    cd.Mcd, 
    cd.Ten_chu_de;
-----------------------------
SELECT 
    cd.Mcd,
    cd.Ten_chu_de,
    COUNT(s.Ms) AS SoLuongSach
FROM
    CHU_DE cd
INNER JOIN
    Sach s ON cd.Mcd = s.Mcd
GROUP BY
    cd.Mcd,
    cd.Ten_chu_de;
----------------------------------
SELECT
    s.Ms,
    s.Ten_sach,
    cd.Ten_chu_de
FROM
    SACH s
INNER JOIN
    CHU_DE cd ON s.Mcd = cd.Mcd
WHERE
    cd.Mcd = '5';
------------------------------
SELECT TOP 5
    s.Ms,
    s.Ten_sach,
    s.Hinh_minh_hoa,
    s.Ngay_cap_nhat
FROM
    SACH s
ORDER BY 
    s.Ngay_cap_nhat DESC;
-------------------------------------
SELECT TOP 5
    s.Ms,
    s.Ten_sach,
    s.Hinh_minh_hoa,
   SUM(ct.So_luong) AS TongSoLuongBan
FROM
    SACH s
INNER JOIN
    CT_DAT_HANG ct ON s.Ms = ct.Ms
GROUP BY
    s.Ms,
    s.Ten_sach,
    s.Hinh_minh_hoa
ORDER BY
    TongSoLuongBan DESC;
--------------------------------
SELECT 
    STT, 
    TenCTy, 
    Hinh_Minh_Hoa, 
    HREF,
    Ngay_bat_dau,
    Ngay_het_han
FROM 
    QUANG_CAO
WHERE 
    Ngay_het_han >= 2020-09-15;
-------------------------------
SELECT 
    tg.Mtg,
    tg.Ten_tac_gia, 
    tg.Dia_chi, 
    tg.Dien_thoai
FROM 
    TAC_GIA tg
INNER JOIN 
    THAM_GIA t ON tg.Mtg = t.Mtg
WHERE 
    t.Ms = 2;
-------------------------------------
SELECT 
    Sdh, 
    Mkh, 
    Ngay_dat_hang, 
    Ngay_giao_hang, 
    Tri_gia,
    Da_giao_hang
FROM 
    DON_DAT_HANG
WHERE 
    Da_giao_hang = 1