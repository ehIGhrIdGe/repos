Create Table quiz_db.dbo.category(
	category_id int not null,
	category_name nvarchar(50) not null,
	constraint [pk_category] primary key clustered([category_id] ASC)
);

Create Table quiz_db.dbo.quiz(
	quiz_id int not null,
	category_id int not null references quiz_db.dbo.category(category_id),
	quiestion nvarchar(max) not null,
	answer int not null,
	comment nvarchar(max) null,
	update_user nvarchar(50) not null,
	update_datetime datetime2 not null,
	constraint [pk_quiz] primary key clustered(quiz_id ASC)
);

Create Table quiz_db.dbo.choices(
	quiz_id int not null references quiz_db.dbo.quiz(quiz_id),
	choices_id int not null,
	disp_value nvarchar(50) not null,
	constraint [pk_choices] primary key clustered(quiz_id ASC, choices_id ASC)
)