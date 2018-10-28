create table shops (id serial primary key, name text, category text, x real, y real);

create table users (id serial primary key, nickname text, avatar_path text, raiting real);

create table favorites (id serial, user_id int REFERENCES users(id) ON DELETE CASCADE, shop_id int REFERENCES shops(id) ON DELETE CASCADE);