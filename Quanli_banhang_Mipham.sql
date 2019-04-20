----- Tạo database Quản lí bán hàng mĩ phẩm 
CREATE DATABASE QLBHMP;
USE QLBHMP;
----- Tạo các bảng của Database 
-----
-----
----- Bảng tbl Nhanvien  
CREATE TABLE tblNhanvien (
  sMaNV varchar(10) NOT NULL PRIMARY KEY, 
  sTenNV nvarchar(30), 
  bGioitinh bit, 
  sDiachi nvarchar(40), 
  dNgaysinh datetime, 
  ) 
  -- Kí hiệu 1-NAM , 0-NỮ 
-- Kiểm tra tuổi của nhân viên phải trên 18 
----- Bảng tbl Khachhang
CREATE TABLE tblKhachhang (
  sMaKH varchar(10) NOT NULL PRIMARY KEY, 
  sTenKH nvarchar(30), 
  sDiachi nvarchar(40), 
  sSDT varchar(11), 
  )
   ----- Bảng tbl Hang 
CREATE TABLE tblHang (
  sMahang varchar(10) NOT NULL PRIMARY KEY, 
  sTenhang nvarchar(20), 
  sMaloaihang varchar(10), 
  iSoluong int, 
  fGiaban float, 
  sGhichu nvarchar(40), 
  ) 
  ----- Bảng tbl Hoadon 
CREATE TABLE tblHoadon (
  sMaHD varchar(10) NOT NULL PRIMARY KEY, 
  sMaNV varchar(10), 
  dNgayban datetime, 
  sMaKH varchar(10), 
  ) 
  ---- Bảng tbl Loaihang 
CREATE TABLE tblLoaihang (
  sMaloaihang varchar(10) NOT NULL PRIMARY KEY, 
  sTenloaihang nvarchar(30), 
  )
   ---- Bảng tbl ChitietHD 
CREATE TABLE tblChitietHD (
  sMaHD varchar(10) NOT NULL, 
  sMahang varchar(10) NOT NULL, 
  iSoluongban int, 
  fDongia float, 
  fGiamgia float, 
  ) 
ALTER TABLE 
  tblChitietHD 
ADD 
  CONSTRAINT PK_chitietHD PRIMARY KEY (sMaHD, sMahang);
ALTER TABLE 
  tblChitietHD 
ADD 
  CONSTRAINT FK_chitietHD_HD FOREIGN KEY (sMaHD) REFERENCES tblHoadon (sMaHD);
ALTER TABLE 
  tblChitietHD 
ADD 
  CONSTRAINT FK_chitietHD_Hang FOREIGN KEY (sMahang) REFERENCES tblHang (sMahang);
----- Khóa phụ FK_Hang_LoaiHang
ALTER TABLE 
  tblHang 
ADD 
  CONSTRAINT FK_Hang_Loaihang FOREIGN KEY (sMaloaihang) REFERENCES tblLoaihang (sMaloaihang);
---- Khóa phụ FK_Hoadon_Khachhang
ALTER TABLE 
  tblHoadon 
ADD 
  CONSTRAINT FK_Hoadon_Khachhang FOREIGN KEY (sMaKH) REFERENCES tblKhachhang (sMaKH);
ALTER TABLE 
  tblHoadon 
ADD 
  CONSTRAINT FK_Hoadon_Nhanvien FOREIGN KEY (sMaNV) REFERENCES tblNhanvien (sMaNV);
--- tạo thủ tục lấy loại hàng
create proc laylh as begin 
select 
  * 
from 
  tblLoaihang;
end 
--- proc lấy thông tin loại hàng theo mã
create proc laylhma @ma nvarchar(10) as begin 
select 
  * 
from 
  tblLoaihang 
where 
  sMaloaihang = @ma;
end 
--- proc lấy thông tin khách hàng theo mã
create proc laykhma @ma nvarchar(10) as begin 
select 
  * 
from 
  tblKhachhang 
where 
  sMaKH = @ma;
end 
---- Loại hàng ( select,selectone,thêm,sửa,xóa)
alter proc LoaiHang @malh nvarchar(50) = null, 
@tenlh nvarchar(50) = null, 
@action varchar(100) = null as begin if(@action = 'select') begin 
select 
  * 
from 
  tblLoaihang;
end if(@action = 'insert') begin insert into tblLoaihang 
values 
  (@malh, @tenlh) end if(@action = 'update') begin 
update 
  tblLoaihang 
set 
  sTenloaihang = @tenlh 
where 
  sMaloaihang = @malh end if(@action = 'search') begin 
select 
  * 
from 
  tblLoaihang 
where 
  sMaloaihang like @malh 
  or sTenloaihang like @malh end if(@action = 'selectone') begin 
select 
  * 
from 
  tblLoaihang 
where 
  sMaloaihang = @malh;
end if(@action = 'delete') begin 
delete from 
  tblLoaihang 
where 
  sMaloaihang = @malh end;
end 
---- Proc Khách Hàng (select,selectone,thêm,sửa,xóa, thêm 1)
alter proc KhachHang @ma nvarchar(50) = null, 
@ten nvarchar(50) = null, 
@diachi nvarchar(50) = null, 
@sdt nvarchar(11) = null, 
@gt bit = null, 
@action varchar(100) = null as begin if(@action = 'select') begin 
select 
  * 
from 
  tblKhachhang;
end if(@action = 'insert') begin insert into tblKhachhang 
values 
  (@ma, @ten, @diachi, @sdt, @gt);
end if(@action = 'update') begin 
update 
  tblKhachhang 
set 
  sTenKH = @ten, 
  sDiachi = @diachi, 
  sSDT = @sdt, 
  bGioiTinh = @gt 
where 
  sMaKH = @ma end if(@action = 'selectone') begin 
select 
  * 
from 
  tblKhachhang 
where 
  sMaKH = @ma;
end if(@action = 'search') begin 
select 
  * 
from 
  tblKhachhang 
where 
  sMaKH like @ma 
  or sTenKH like @ma 
  or sDiachi like @ma 
  or sSDT like @ma;
end if(@action = 'delete') begin 
delete from 
  tblKhachhang 
where 
  sMaKH = @ma;
end;
end 
----- Thông tin của nhân viên(select, select one, insert, update, delete )
alter proc NhanVien @ma nvarchar(30) = null, 
@ten nvarchar(50) = null, 
@gt bit = null, 
@diachi nvarchar(50) = null, 
@ngaysinh datetime = null, 
@tk nvarchar(50) = null, 
@mk nvarchar(30) = null, 
@sdt nvarchar(11) = null, 
@trangthai nvarchar(10) = null, 
@action varchar(100) = null as begin if(@action = 'select') begin 
select 
  * 
from 
  tblNhanvien;
end if(@action = 'insert') begin insert into tblNhanvien 
values 
  (
    @ma, @ten, @gt, @diachi, @ngaysinh, 
    @tk, @mk, @trangthai, @sdt
  );
end if(@action = 'update') begin 
update 
  tblNhanvien 
set 
  sTenNV = @ten, 
  bGioitinh = @gt, 
  sDiachi = @diachi, 
  dNgaysinh = @ngaysinh, 
  sTaiKhoan = @tk, 
  sMatKhau = @mk, 
  sSdt = @sdt, 
  sTrangThai = @trangthai 
where 
  sMaNV = @ma end if(@action = 'doimk') begin 
update 
  tblNhanvien 
set 
  sTaiKhoan = @tk, 
  sMatKhau = @mk 
where 
  sMaNV = @ma end if(@action = 'search') begin 
select 
  * 
from 
  tblNhanvien 
where 
  sMaNV like @ma 
  or sTenNV like @ma 
  or sTaiKhoan like @ma 
  or sMatKhau like @ma 
  or sDiachi like @ma 
  or sSdt like @ma 
  or dNgaysinh like @ma end if(@action = 'selectnv') begin 
select 
  * 
from 
  tblNhanvien 
where 
  sTrangThai = 'nhanvien';
end if(@action = 'selectkhoa') begin 
select 
  * 
from 
  tblNhanvien 
where 
  sTrangThai = 'khoa';
end if(@action = 'selectone') begin 
select 
  * 
from 
  tblNhanvien 
where 
  sMaNV = @ma;
end if(@action = 'selecttk') begin 
select 
  * 
from 
  tblNhanvien 
where 
  sTaiKhoan = @ma;
end if(@action = 'kiemtradangnhap') begin 
select 
  * 
from 
  tblNhanvien 
where 
  sTaiKhoan = @tk 
  and sMatKhau = @mk;
end if(@action = 'delete') begin 
delete from 
  tblNhanvien 
where 
  sMaNV = @ma;
end if(@action = 'khoatk') begin 
update 
  tblNhanvien 
set 
  sTrangThai = N 'khoa' 
where 
  sMaNV = @ma;
end if(@action = 'motk') begin 
update 
  tblNhanvien 
set 
  sTrangThai = N 'nhanvien' 
where 
  sMaNV = @ma;
end end 
select 
  * 
from 
  tblChitietHD 
select 
  * 
from 
  tblHoadon 
  --- proc Mỹ phẩm(Lấy, Lấy 1,Thêm, sửa , xóa)
  alter proc MyPham @ma nvarchar(10) = null, 
  @ten nvarchar(50) = null, 
  @maloai nvarchar(50)= null, 
  @soluong int = null, 
  @dongia float = null, 
  @action nvarchar(30) = null as begin if(@action = 'select') begin 
select 
  [sTenloaihang], 
  [sMahang], 
  [sTenhang], 
  [iSoluong], 
  [fDongia] 
from 
  tblHang 
  inner join tblLoaihang on tblLoaihang.sMaloaihang = tblHang.sMaloaihang;
end if(@action = 'insert') begin insert into tblHang 
values 
  (
    @ma, @ten, @maloai, @soluong, @dongia
  );
end if(@action = 'update') begin 
update 
  tblHang 
set 
  sTenhang = @ten, 
  sMaloaihang = @maloai, 
  iSoluong = @soluong, 
  fDongia = @dongia 
where 
  sMahang = @ma end if(@action = 'selectone') begin 
select 
  * 
from 
  tblHang 
where 
  sMahang = @ma;
end if(@action = 'search') begin 
select 
  [sTenloaihang], 
  [sMahang], 
  [sTenhang], 
  [iSoluong], 
  [fDongia] 
from 
  tblHang 
  inner join tblLoaihang on tblHang.sMaloaihang = tblLoaihang.sMaloaihang 
where 
  sMahang like @ma 
  or sTenhang like @ma 
  or tblLoaihang.sTenloaihang like @ma 
  or iSoluong like @ma 
  or tblHang.sMaloaihang like @ma 
  or fDongia like @ma;
end if(@action = 'delete') begin 
delete from 
  tblHang 
where 
  sMahang = @ma;
end end 
----proc kiem tra so luong
alter proc sp_ktsl @ma nvarchar(20) as begin 
select 
  iSoluong 
from 
  tblHang 
where 
  sMahang like @ma end execute sp_ktsl "H1" 
  ----proc kiem tra don gia
  alter proc sp_ktdg @ma nvarchar(20) as begin 
select 
  fDongia 
from 
  tblHang 
where 
  sMahang like @ma end 
  ---- proc lay thong tin hoa don
  create proc getHoaDon @mahd nvarchar(20) as begin 
select 
  tblHoadon.[sMaHD], 
  tblHoadon.[sMaNV], 
  tblKhachhang.[sMaKH], 
  sTenKH, 
  tblKhachhang.sDiachi, 
  tblKhachhang.sSDT, 
  tblKhachhang.bGioitinh, 
  tblHang.[sMahang], 
  sTenhang, 
  tblHang.sMaloaihang, 
  sTenloaihang, 
  iSoluong, 
  fDongia, 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia], 
  [dNgayban], 
  tblNhanvien.sTenNV 
from 
  tblHoadon 
  inner join tblChitietHD on tblHoadon.sMaHD = tblChitietHD.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV 
  inner join tblLoaihang on tblLoaihang.sMaloaihang = tblHang.sMaloaihang 
where 
  tblHoadon.sMaHD = @mahd;
end 
---- proc thêm sửa xóa trên bảng hóa đơn, chi tiết hóa đơn
---- execute HoaDon @mahd='HD1',@action='selectone'
alter proc HoaDon @mahd nvarchar(20) = null, 
@manv nvarchar(50) = null, 
@makh nvarchar(50)= null, 
@mahang nvarchar(50)= null, 
@ngayban datetime = null, 
@ngaybatdau datetime = null, 
@giabandau float = null, 
@giaketthuc float = null, 
@soluong int = null, 
@giaban float = null, 
@giamgia float = null, 
@action nvarchar(30) = null as begin if(@action = 'select') begin 
select 
  tblHoadon.[sMaHD], 
  [sTenNV], 
  [sTenKH], 
  [sTenhang], 
  [dNgayban], 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia] 
from 
  tblChitietHD 
  inner join tblHoadon on tblChitietHD.sMaHD = tblHoadon.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV;
end if(@action = 'selectdate') begin 
select 
  tblHoadon.[sMaHD], 
  [sTenNV], 
  [sTenKH], 
  [sTenhang], 
  [dNgayban], 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia] 
from 
  tblChitietHD 
  inner join tblHoadon on tblChitietHD.sMaHD = tblHoadon.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV 
where 
  [dNgayban] > @ngaybatdau 
  and [dNgayban] < @ngayban;
end 
if(@action = 'selectgia') begin 
select 
  tblHoadon.[sMaHD], 
  [sTenNV], 
  [sTenKH], 
  [sTenhang], 
  [dNgayban], 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia] 
from 
  tblChitietHD 
  inner join tblHoadon on tblChitietHD.sMaHD = tblHoadon.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV 
where 
  (
    ([fGiaban] * [iSoluongban])-(
      [fGiaban] * [iSoluongban] *(fGiamgia / 100)
    )
  ) > @giabandau 
  and (
    ([fGiaban] * [iSoluongban])-(
      [fGiaban] * [iSoluongban] *(fGiamgia / 100)
    )
  ) < @giaketthuc;
end
if(@action = 'selecthaidk') begin 
select 
  tblHoadon.[sMaHD], 
  [sTenNV], 
  [sTenKH], 
  [sTenhang], 
  [dNgayban], 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia] 
from 
  tblChitietHD 
  inner join tblHoadon on tblChitietHD.sMaHD = tblHoadon.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV 
where 
  (
    ([fGiaban] * [iSoluongban])-(
      [fGiaban] * [iSoluongban] *(fGiamgia / 100)
    )
  ) > @giabandau 
  and (
    ([fGiaban] * [iSoluongban])-(
      [fGiaban] * [iSoluongban] *(fGiamgia / 100)
    )
  ) < @giaketthuc
  and 
  [dNgayban] > @ngaybatdau 
  and [dNgayban] < @ngayban;
  ;
end
 if(@action = 'search') begin 
select 
  tblHoadon.[sMaHD], 
  [sTenNV], 
  [sTenKH], 
  [sTenhang], 
  [dNgayban], 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia] 
from 
  tblChitietHD 
  inner join tblHoadon on tblChitietHD.sMaHD = tblHoadon.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV 
where 
  tblHoadon.[sMaHD] like @mahd 
  or [sTenNV] like @mahd 
  or [sTenKH] like @mahd 
  or [sTenhang] like @mahd 
  or [dNgayban] like @mahd 
  or [fGiaban] like @mahd 
  or [iSoluongban] like @mahd 
  or [fGiamgia] like @mahd;
end if(@action = 'insert') begin insert into tblHoadon 
values 
  (@mahd, @manv, @ngayban, @makh);
insert into tblChitietHD 
values 
  (
    @mahd, @mahang, @soluong, @giaban, 
    @giamgia
  );
end if(@action = 'update') begin 
update 
  tblHoadon 
set 
  [sMaNV] = @manv, 
  [dNgayban] = @ngayban, 
  [sMaKH] = @makh 
where 
  [sMaHD] = @mahd 
update 
  tblChitietHD 
set 
  [sMahang] = @mahang, 
  [iSoluongban] = @soluong, 
  [fGiaban] = @giaban, 
  [fGiamgia] = @giamgia 
where 
  [sMaHD] = @mahd end if(@action = 'selectone') begin 
select 
  tblHoadon.[sMaHD], 
  tblHoadon.[sMaNV], 
  tblKhachhang.[sMaKH], 
  sTenKH, 
  tblKhachhang.sDiachi, 
  tblKhachhang.sSDT, 
  tblKhachhang.bGioitinh, 
  tblHang.[sMahang], 
  sTenhang, 
  tblHang.sMaloaihang, 
  sTenloaihang, 
  iSoluong, 
  fDongia, 
  [fGiaban], 
  [iSoluongban], 
  [fGiamgia], 
  [dNgayban], 
  tblNhanvien.sTenNV 
from 
  tblHoadon 
  inner join tblChitietHD on tblHoadon.sMaHD = tblChitietHD.sMaHD 
  inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
  inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV 
  inner join tblLoaihang on tblLoaihang.sMaloaihang = tblHang.sMaloaihang 
where 
  tblHoadon.sMaHD = @mahd;
end if(@action = 'delete') begin 
delete from 
  tblHoadon 
where 
  sMaHD = @mahd;
end end

select* from tblHoadon
exec HoaDon @action = 'selectgia', 
@giabandau = 1000, 
@giaketthuc = 10000 
exec HoaDon @action = 'selectdate', 
@ngaybatdau = '1999-09-10', 
@ngayban = '2019-11-10' 

  ---- proc khóa tài khoản của 1 nhân viên
  alter proc khoatk @ma nvarchar(10) as begin 
update 
  tblNhanvien 
SET 
  sTrangThai = 'khoa' 
where 
  sMaNV = @ma;
end 
---- thống kê hàng theo loại hàng
create proc tkloaihang as begin 
select 
  tblLoaihang.sMaloaihang, 
  sTenloaihang, 
  count(sMahang) as [Số lượng mặt hàng], 
  sum(iSoluong * fDongia) as [Tổng tiền] 
from 
  tblLoaihang, 
  tblHang 
where 
  tblLoaihang.sMaloaihang = tblHang.sMaloaihang 
group by 
  sTenloaihang, 
  tblLoaihang.sMaloaihang end
   ---- proc mở khóa tài khoản
  create proc motk @ma nvarchar(10) as begin 
update 
  tblNhanvien 
SET 
  sTrangThai = 'nhanvien' 
where 
  sMaNV = @ma;
end ---- 
alter PROC sp_session @manv NVARCHAR(20), 
@trangthai nvarchar(20) AS BEGIN 
DELETE FROM 
  tbl_Session 
WHERE 
  1 = 1;
INSERT INTO tbl_Session 
VALUES 
  (@manv, @trangthai);
END 
--- Lấy thông tin từ bảng session
create proc get_session as begin 
Select 
  * 
from 
  tbl_Session;
end 
---tạo proc lấy thông tin nhân viên từ bảng session
create proc sp_nvdn as begin 
select 
  * 
from 
  tblNhanvien 
  inner join tbl_Session on tbl_Session.sMaNV = tblNhanvien.sMaNV end 
select 
  * 
from 
  tblHoadon 
select 
  * 
from 
  tblChitietHD alter proc sp_inkh as begin 
select 
  sTenKH, 
  tblKhachhang.sMaKH, 
  sDiachi, 
  sSDT, 
  bGioiTinh, 
  sum(
    (fGiaban * iSoluongban) - (
      (fGiaban * iSoluongban) * (fGiamgia / 100)
    )
  ) as [Thành tiền] 
from 
  tblHoadon, 
  tblKhachhang, 
  tblChitietHD 
where 
  tblKhachhang.sMaKH = tblHoadon.sMaKH 
  and tblHoadon.sMaHD = tblChitietHD.sMaHD 
group by 
  sTenKH, 
  tblKhachhang.sMaKH, 
  sDiachi, 
  sSDT, 
  bGioiTinh end 
  --- tạo proc thêm sửa xóa chi tiết hóa đơn
  alter proc CTHD @mahd nvarchar(20) = null, 
  @mahang nvarchar(20) = null, 
  @sl int = null, 
  @giaban float = null, 
  @giamgia float = null, 
  @action nvarchar(20) = null as begin if (@action = 'select') begin 
select 
  * 
from 
  tblChitietHD;
end if (@action = 'selectone') begin 
select 
  * 
from 
  tblChitietHD 
where 
  sMaHD = @mahd;
end if (@action = 'insert') begin insert tblChitietHD 
values 
  (
    @mahd, @mahang, @sl, @giaban, @giamgia
  );
end if (@action = 'update') begin 
update 
  tblChitietHD 
set 
  sMahang = @mahang, 
  iSoluongban = @sl, 
  fGiaban = @giaban, 
  fGiamgia = @giamgia 
where 
  sMaHD = @mahd;
end if(@action = 'delete') begin 
delete from 
  tblChitietHD 
where 
  sMaHD = @mahd;
end end ---- proc lấy thông tin của hóa đơn
select 
  * 
from 
  tblHoadon alter proc tthoadon @mahd nvarchar(20) = null, 
  @manv nvarchar(20) = null, 
  @ngayban datetime = null, 
  @makh nvarchar(20) = null, 
  @action nvarchar(20) = null as begin if(@action = 'select') begin 
select 
  sMaHD, 
  sTenNV, 
  sTenKH, 
  dNgayban 
from 
  tblHoadon 
  inner join tblNhanvien on tblHoadon.sMaNV = tblNhanvien.sMaNV 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH;
end if(@action = 'insert') begin insert into tblHoadon 
values 
  (@mahd, @manv, @ngayban, @makh) end if(@action = 'update') begin 
update 
  tblHoadon 
set 
  dngayban = @ngayban, 
  sMaKH = @makh 
where 
  sMaHD = @mahd end if(@action = 'search') begin 
select 
  sMaHD, 
  sTenNV, 
  sTenKH, 
  dNgayban 
from 
  tblHoadon 
  inner join tblNhanvien on tblHoadon.sMaNV = tblNhanvien.sMaNV 
  inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH 
where 
  sMaHD like @mahd 
  or tblHoadon.sMaNV like @mahd 
  or tblNhanvien.sTenNV like @mahd end if(@action = 'delete') begin 
delete from 
  tblHoadon 
where 
  sMaHD = @mahd end end 
  ---- tạo proc thêm thông tin của chi tiết hóa đơn
select 
  * 
from 
  tblChitietHD alter proc add_ctHoaDon @mahd nvarchar(20) = null, 
  @mahang nvarchar(20) = null, 
  @slban int = null, 
  @giaban float = null, 
  @giamgia float = null, 
  @action nvarchar(20) = null as begin if(@action = 'select') begin 
select 
  * 
from 
  tblChitietHD 
  inner join tblHang on tblHang.sMahang = tblChitietHD.sMahang;
end if(@action = 'selectone') begin 
select 
  * 
from 
  tblChitietHD 
  inner join tblHang on tblHang.sMahang = tblChitietHD.sMahang 
where 
  tblChitietHD.sMahang = @mahang 
  and tblChitietHD.sMaHD = @mahd;
end if(@action = 'selectallinone') begin 
select 
  tblChitietHD.sMaHD, 
  tblChitietHD.sMahang, 
  sTenhang, 
  iSoluongban, 
  fGiaban, 
  fGiamgia 
from 
  tblChitietHD 
  inner join tblHang on tblHang.sMahang = tblChitietHD.sMahang 
where 
  sMaHD = @mahd;
end if(@action = 'insert') begin insert into tblChitietHD 
values 
  (
    @mahd, @mahang, @slban, @giaban, @giamgia
  ) end if(@action = 'update') begin 
update 
  tblChitietHD 
set 
  sMahang = @mahang, 
  iSoluongban = @slban, 
  fGiaban = @giaban, 
  fGiamgia = @giamgia 
where 
  sMaHD = @mahd 
  and sMahang = @mahang end if(@action = 'delete') begin 
delete from 
  tblChitietHD 
where 
  sMaHD = @mahd end end 
select 
  * 
from 
  tblChitietHD 
select 
  * 
from 
  tbl_mathang create proc getHang @ma int as begin 
select 
  * 
from 
  tbl_mathang 
where 
  mahang = @ma end --- cập nhật trang thái đăng nhập
  create proc changedn @ma int as begin 
update 
  tbl_Session 
set 
  sTrangThai = @ma end ---  proc thong ke
