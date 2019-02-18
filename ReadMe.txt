Form4 用DataGridView 來顯示圖片


Form1 即時更新DataGridView
原理就是用C#FileSystemWatcher 來監視MySql檔案
有變動就查表然後更新DataGridView

請按照下方來建置MySql DB、Table

create database db;

use db;

create table Student_Info(Student_Id varchar(50), 
			 Student_Name varchar(50) NOT NULL,
			 Age int NOT NULL,
			 Validation BOOLEAN NOT NULL,
			 primary key(Student_Id));

insert into student_info value('10424130','Steven_Wong','22','1');
insert into student_info value('10424142','Lin_Bo_Wei','22','1');
insert into student_info value('10328028','XU_ZHNA_WEI','23','1');
insert into student_info value('10424220','Jack','22','1');

create table Card_Info(	Card_Id varchar(50),
			Student_Id varchar(50) not null,
			primary key(Card_Id),
			foreign key(Student_Id) references Student_Info(Student_Id));

insert into Card_Info value('ABCDEFG','10328028');

create table Face_Info(	Face_Id varchar(50),
			Face_Directory varchar(500) not null,
			Student_Id varchar(50) not null,
			primary key(Face_Id),
			foreign key(Student_Id) references Student_Info(Student_Id));

insert into face_info value('Jack01','Jack01','10424220');
insert into face_info value('Jack02','Jack02','10424220');
insert into face_info value('Jack03','Jack03','10424220');
insert into face_info value('Jack04','Jack04','10424220');
insert into face_info value('PoWei01','PoWei01','10424142');
insert into face_info value('PoWei02','PoWei02','10424142');
insert into face_info value('PoWei03','PoWei03','10424142');
insert into face_info value('PoWei04','PoWei04','10424142');
insert into face_info value('Steven01','Steven01','10424130');
insert into face_info value('Steven02','Steven02','10424130');
insert into face_info value('Steven03','Steven03','10424130');
insert into face_info value('Steven04','Steven04','10424130');
insert into face_info value('XU_ZHNA_WEI','XU_ZHNA_WEI','10328028');
insert into face_info value('XU_ZHNA_WEI2','XU_ZHNA_WEI2','10328028');
insert into face_info value('XU_ZHNA_WEI3','XU_ZHNA_WEI3','10328028');
insert into face_info value('XU_ZHNA_WEI4','XU_ZHNA_WEI4','10328028');

create table Scan_Face_Info(Face_Record int auto_increment,
			  Face_Id varchar(50) not null,
			  Date_Time DATETIME not null,
			  primary key(Face_Record),
			  foreign key(Face_Id) references Face_Info(Face_Id));
        
/*用這一行新增資料，並同時觀看Form1的DataGridView的更新*/
insert into Scan_Face_Info(Face_Id,Date_Time) value('Jack01',now());

		   
create table Scan_Card_Info(Card_Record int auto_increment,
			  Card_Id varchar(50) not null,
			  Date_Time DATETIME not null,
			  Photo_Directory char(100) not null,
			  primary key(Card_Record),
			  foreign key(Card_Id) references Card_Info(Card_Id));

insert into Scan_Card_Info(Card_Id ,Date_Time,Photo_Directory) value('ABCDEFG',NOW(),'demo.jpg');
