select * from category
select * from quiz
select * from choices

insert into quiz values(2,3, 'ビールかけを最初にしたのはどの球団の選手？', 4, null , SUSER_NAME(), SYSDATETIME());
insert into choices values(2,1,'読売ジャイアンツ');
insert into choices values(2,2,'大阪タイガース（現：阪神タイガース）');
insert into choices values(2,3,'草野球チーム');
insert into choices values(2,4,'南海ホークス（現：福岡ソフトバンクホークス）');

select * from quiz
select * from choices
