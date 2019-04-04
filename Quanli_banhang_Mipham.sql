
----- Tạo database Quản lí bán hàng mĩ phẩm 
CREATE DATABASE QLBHMP ;
USE QLBHMP ;

----- Tạo các bảng của Database 
-----
-----
----- Bảng tbl Nhanvien  

CREATE TABLE tblNhanvien (
	sMaNV     varchar(10) NOT NULL PRIMARY KEY  ,
	sTenNV    nvarchar(30)                      , 
	bGioitinh bit                               ,
	sDiachi   nvarchar(40)                      ,
	dNgaysinh datetime                          ,
)
-- Kí hiệu 1-NAM , 0-NỮ 
-- Kiểm tra tuổi của nhân viên phải trên 18 
----- Bảng tbl Khachhang

CREATE TABLE tblKhachhang (
	sMaKH     varchar(10)  NOT NULL PRIMARY KEY   ,
	sTenKH    nvarchar(30)                        ,
	sDiachi   nvarchar(40)                        , 
	sSDT      varchar(11)                         ,

)

----- Bảng tbl Hang 

CREATE TABLE tblHang ( 
	sMahang     varchar(10)   NOT NULL PRIMARY KEY    ,
	sTenhang    nvarchar(20)                          , 
	sMaloaihang varchar(10)                           , 
	iSoluong    int                                   ,
	fGiaban     float                                 , 
	sGhichu     nvarchar(40)                          , 
)



----- Bảng tbl Hoadon 

CREATE TABLE tblHoadon (
	sMaHD varchar(10) NOT NULL PRIMARY KEY    ,
	sMaNV varchar(10)                         , 
	dNgayban datetime                         ,
	sMaKH varchar(10)                         , 


)

---- Bảng tbl Loaihang 
CREATE TABLE tblLoaihang (
	sMaloaihang varchar(10)     NOT NULL PRIMARY KEY  , 
	sTenloaihang nvarchar(30)                         ,
)

---- Bảng tbl ChitietHD 

CREATE TABLE tblChitietHD (
	sMaHD varchar(10)    NOT NULL            ,
	sMahang varchar(10)  NOT NULL            ,
	iSoluongban  int                         ,
	fDongia float                            ,
	fGiamgia float                           , 

)
ALTER TABLE tblChitietHD ADD CONSTRAINT PK_chitietHD
PRIMARY KEY ( sMaHD , sMahang )  ;

ALTER TABLE tblChitietHD ADD CONSTRAINT FK_chitietHD_HD 
FOREIGN KEY (sMaHD) REFERENCES tblHoadon (sMaHD ) ;

ALTER TABLE tblChitietHD ADD CONSTRAINT FK_chitietHD_Hang 
FOREIGN KEY (sMahang) REFERENCES tblHang (sMahang ) ;

----- Khóa phụ FK_Hang_LoaiHang
ALTER TABLE tblHang 
ADD CONSTRAINT FK_Hang_Loaihang FOREIGN KEY  (sMaloaihang ) REFERENCES tblLoaihang ( sMaloaihang ) ;

---- Khóa phụ FK_Hoadon_Khachhang

ALTER TABLE tblHoadon ADD CONSTRAINT FK_Hoadon_Khachhang
FOREIGN KEY ( sMaKH ) REFERENCES tblKhachhang ( sMaKH ) ;

ALTER TABLE tblHoadon ADD CONSTRAINT FK_Hoadon_Nhanvien
FOREIGN KEY ( sMaNV ) REFERENCES tblNhanvien ( sMaNV ) ;


 --- tạo thủ tục lấy loại hàng
 create proc laylh
 	as
 	begin
 	select * from tblLoaihang;
 	end
 --- proc lấy thông tin loại hàng theo mã
 create proc laylhma @ma nvarchar(10)
 	as
 	begin
 	select * from tblLoaihang where sMaloaihang = @ma;
 	end
 --- proc lấy thông tin khách hàng theo mã
 create proc laykhma @ma nvarchar(10)
 	as
 	begin
 	select * from tblKhachhang where sMaKH = @ma;
 	end
 ---- Loại hàng ( select,selectone,thêm,sửa,xóa)
 alter proc LoaiHang
 @malh nvarchar(50) = null,
 @tenlh nvarchar(50) = null,
 @action varchar(100) = null
 as
 begin
 if(@action = 'select')
 begin
 select * from tblLoaihang;
 end
 if(@action ='insert')
 begin
 insert into tblLoaihang values(@malh, @tenlh)
 	end
 	if(@action = 'update')
 	begin
 	update tblLoaihang set sTenloaihang = @tenlh where sMaloaihang = @malh
 		end
 		if(@action ='selectone')
 		begin
 		select * from tblLoaihang where sMaloaihang = @malh;
 		end	
 		if(@action = 'delete')
 		begin
 		delete from tblLoaihang where sMaloaihang = @malh
 			end;
 			end
---- Proc Khách Hàng (select,selectone,thêm,sửa,xóa, thêm 1)
alter proc KhachHang
@ma nvarchar(50) = null,
@ten nvarchar(50) = null,
@diachi nvarchar(50) = null,
@sdt nvarchar(11) = null,
@gt bit=null,
@action varchar(100) = null
as
begin
if(@action = 'select')
begin
select * from tblKhachhang;
end
if(@action ='insert')
begin
insert into tblKhachhang values(@ma, @ten,@diachi,@sdt,@gt);
	end
	if(@action = 'update')
	begin
	update tblKhachhang set sTenKH = @ten ,sDiachi = @diachi,sSDT=@sdt,bGioiTinh=@gt where sMaKH = @ma
		end
		if(@action ='selectone')
		begin
		select * from tblKhachhang where sMaKH = @ma;
		end	
		if(@action = 'delete')
		begin
		delete from tblKhachhang where sMaKH = @ma;
			end;
			end
----- Thông tin của nhân viên(select, select one, insert, update, delete )
alter proc NhanVien
@ma nvarchar(30) = null,
@ten nvarchar(50) = null,
@gt bit=null,
@diachi nvarchar(50) = null,
@ngaysinh datetime = null,
@tk nvarchar(50) = null,
@mk nvarchar(30) = null,
@sdt nvarchar(11) = null,
@trangthai nvarchar(10) = null,
@action varchar(100) = null
as
begin
if(@action = 'select')
begin
select * from tblNhanvien;
end
if(@action ='insert')
begin
insert into tblNhanvien values(@ma, @ten,@gt,@diachi,@ngaysinh,@tk,@mk,@trangthai,@sdt);
	end
	if(@action = 'update')
	begin
	update tblNhanvien set sTenNV = @ten,bGioitinh=@gt ,sDiachi =@diachi,dNgaysinh=@ngaysinh,sTaiKhoan=@tk,sMatKhau= @mk,sSdt = @sdt,sTrangThai = @trangthai where sMaNV = @ma
		end
		if(@action ='selectone')
		begin
		select * from tblNhanvien where sMaNV = @ma;
		end
		if(@action ='selecttk')
		begin
		select * from tblNhanvien where sTaiKhoan = @ma;
		end	
		if(@action ='kiemtradangnhap')
		begin
		select * from tblNhanvien where sTaiKhoan = @tk and sMatKhau = @mk;
		end	
		if(@action = 'delete')
		begin
		delete from tblNhanvien where sMaNV = @ma;
			end
			if(@action = 'khoatk' )
			begin
			update tblNhanvien set sTrangThai = N'khoa' where sMaNV = @ma;
				end
				if(@action = 'motk')
				begin
				update tblNhanvien set sTrangThai = N'nhanvien' where sMaNV = @ma;
					end
					end
					select * from tblChitietHD
					select * from tblHoadon
--- proc Mỹ phẩm(Lấy, Lấy 1,Thêm, sửa , xóa)
alter proc MyPham
@ma nvarchar(10) = null,
@ten nvarchar(50) = null,
@maloai nvarchar(50)=null,
@soluong int=null,
@dongia float=null,
@action nvarchar(30) = null
as
begin
if(@action = 'select')
begin
select [sTenloaihang],[sMahang],[sTenhang],[iSoluong],[fDongia] from tblHang inner join tblLoaihang on tblLoaihang.sMaloaihang  = tblHang.sMaloaihang ;
end
if(@action ='insert')
begin
insert into tblHang values(@ma, @ten,@maloai,@soluong,@dongia);
	end
	if(@action = 'update')
	begin
	update tblHang set sTenhang = @ten, sMaloaihang = @maloai,iSoluong = @soluong,fDongia=@dongia where sMahang = @ma
		end
		if(@action ='selectone')
		begin
		select * from tblHang where sMahang = @ma;
		end	
		if(@action = 'delete')
		begin
		delete from tblHang where sMahang = @ma;
			end
			end
---- proc thêm sửa xóa trên bảng hóa đơn, chi tiết hóa đơn
alter proc HoaDon
@mahd nvarchar(20) = null,
@manv nvarchar(50) = null,
@makh nvarchar(50)=null,
@mahang nvarchar(50)=null,
@ngayban datetime =null,
@soluong int=null,
@giaban float=null,
@giamgia float=null,
@action nvarchar(30) = null
as
begin
if(@action = 'select')
begin
select tblHoadon.[sMaHD],[sTenNV],[sTenKH],[sTenhang],[dNgayban],[fGiaban],[iSoluongban],[fGiamgia] from tblChitietHD inner join tblHoadon on tblChitietHD.sMaHD = tblHoadon.sMaHD inner join tblHang on tblChitietHD.sMahang = tblHang.sMahang inner join tblKhachhang on tblKhachhang.sMaKH = tblHoadon.sMaKH inner join tblNhanvien on tblNhanvien.sMaNV = tblHoadon.sMaNV;
end
if(@action ='insert')
begin	
insert into tblHoadon values(@mahd,@manv,@ngayban,@makh);
	insert into tblChitietHD values(@mahd,@mahang,@soluong,@giaban,@giamgia);
		end
		if(@action = 'update')
		begin
		update tblHoadon set [sMaNV]= @manv,[dNgayban]=@ngayban, [sMaKH]=@makh  where [sMaHD]=@mahd
			update tblChitietHD set [sMahang]= @mahang,[iSoluongban]=@soluong,[fGiaban]=@giaban,[fGiamgia]=@giamgia where [sMaHD]=@mahd
				end
				if(@action ='selectone')
				begin
				select tblHoadon.[sMaHD],[sMaNV],[sMaKH],[sMahang],[fGiaban],[iSoluongban],[fGiamgia] from tblHoadon inner join tblChitietHD on tblHoadon.sMaHD = tblChitietHD.sMaHD where tblHoaDon.sMaHD = @mahd;
				end	
				if(@action = 'delete')
				begin
				delete from tblHoadon where sMaHD = @mahd;
					end
					end
---- proc khóa tài khoản của 1 nhân viên
alter proc khoatk
@ma nvarchar(10)
as
begin
update tblNhanvien SET sTrangThai = 'khoa' where sMaNV = @ma;
	end
---- proc mở khóa tài khoản
create proc motk
	@ma nvarchar(10)
	as
	begin
	update tblNhanvien SET sTrangThai = 'nhanvien' where sMaNV = @ma;
		end

---- 
alter PROC sp_session
	@manv NVARCHAR(20)
AS
	BEGIN
		DELETE FROM tbl_Session WHERE 1=1;
		INSERT INTO tbl_Session VALUES(@manv);
	END
--- Lấy thông tin từ bảng session
create proc get_session
as
begin
 Select * from tbl_Session;
end
