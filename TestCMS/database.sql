create table shops (id serial primary key, name text, category text, x real, y real);

create table users (id serial primary key, nickname text, avatar_path text, raiting real);

create table favorites (user_id int NOT NULL REFERENCES users(id) ON DELETE CASCADE, shop_id int not null REFERENCES shops(id) ON DELETE CASCADE, primary key(user_id,shop_id));