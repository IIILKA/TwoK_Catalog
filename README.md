# TwoK_Catalog

Веб-приложение(интернет-магазин)

## Технологии: 
+ ASP.NET Core
+ Razor
+ EF Core(MS SCL Server)
+ ASP.NET Core Identity

## Функциональность: 
- [X] авторизация
- [X] аутентификация
- [X] просмотр имеющихся товаров
- [X] взаимодействие с корзиной товаров
- [X] оформление заказов
- [X] администрирование товаров, заказов и  пользователей
- [ ] сортировка товаров
- [ ] просмотр новостей
- [ ] администрирование новостей
- [ ] просмотр обзоров
- [ ] администрирование обзоров

(**часть дизайна позаимствована с сайта 1k.by*)

(**реализованная функциональность помечена галочкой*)

(**пользователь с максимальными правами определённый по умолчанию Email: **admin@default.com** Password: **secretPassword** *)

Начальная страница сайта пока что имеет ограниченный функционал и нужна в основном чтобы показать что будет реализовано в будущем

### Пункты начальной страницы
+ Категории
![image](https://user-images.githubusercontent.com/83662114/192184754-cb6bc112-8b6c-4eb1-b826-0b546cb39cee.png)
+ Новости *(без бэкенда)*
![image](https://user-images.githubusercontent.com/83662114/192201465-a59482ec-f59b-4c9b-9ea7-18b256a6b8f5.png)
+ Новинки *(без бэкенда)*
![image](https://user-images.githubusercontent.com/83662114/192201518-aafdc737-8027-4783-88e4-03f5a76fbf5c.png)
+ Обзоры *(без бэкенда)*
![image](https://user-images.githubusercontent.com/83662114/192201633-43689e76-18ae-426b-afa3-718b8f300ab9.png)
+ Header и Footer *(присутствуют на каждой странице не предназначенной администраторам)*
  + Header *(работают только кнопки авторизации, корзины, профиля и лого(ссылка на начальную страницу))*
    + Не авторизованого пользователя
    ![image](https://user-images.githubusercontent.com/83662114/192202321-d482094e-8683-455c-ae10-abe613b4ae2e.png)
    + Авторизованого пользователя
    ![image](https://user-images.githubusercontent.com/83662114/192202472-8e082c19-39c9-4682-8526-11c3f9f47998.png)
  + Footer *(без бэкенда)*
  ![image](https://user-images.githubusercontent.com/83662114/192202532-992ba8bf-495e-4c00-b060-c1b1f3c1aff1.png)


## Описание реализованной функциональности

+ При нажатии на кнопки авторизации появляются соответствующий попап
  + ![image](https://user-images.githubusercontent.com/83662114/192203858-9b912d7e-b56f-4f6e-b2e4-e4a4e858ed21.png)
  + ![image](https://user-images.githubusercontent.com/83662114/192203911-8597876f-c17d-48b0-8a83-86eff490b543.png)
  
если ввести в любой из этих попапов некорректную информацию то появятся соответствующие сообщения об ошибках а поля для **пароля** сбросятся

![image](https://user-images.githubusercontent.com/83662114/192204459-7d9b2d6b-b525-4414-b775-d2f0fe33ea40.png)

В приложении есть три роли: 
  + User *(обычный пользователь, может взаимодействовать с корзиной и совершать заказы)*
  + JuniorAdmin *(младший админ - это User, который может CRUD'ить товары, а так же помечать неотправленные заказы как отправленные)*
  + SeniorAdmin *(старший админ - это младший админ, который CRUD'ить пользователей не являющихся старшими админами)*

*(Анонимы могут просматривать товары, но не могут положить их в корзину)*

+ При нажатии на иконку профиля осуществляется переход на страницу профиля где видны заказы пользователя а так же действия, которые он может совершить
  + User 
  ![image](https://user-images.githubusercontent.com/83662114/192206905-bb1868b1-547a-4668-b30d-44ebc8a4122b.png)
  + JuniorAdmin
  ![image](https://user-images.githubusercontent.com/83662114/192207061-5f273300-fb82-4575-a143-ad13d29ce1e6.png)
  + SeniorAdmin
  ![image](https://user-images.githubusercontent.com/83662114/192207158-ba6c51c3-781a-4234-838d-a0f87d2d2122.png)

+ Посмотреть товары можно кликнув на категорию "Мобильные телефоны", после чего появится страница с товарами
![image](https://user-images.githubusercontent.com/83662114/192207672-240c8057-e601-4fea-a8f1-b34fd20f2079.png)

***(!!!форма сортировки слева пока не работает!!!)***

количество товаров на одной странице определяется параметром `PageSize` в `ProductController`

если кликнуть на изображение товара, его название или стоимость, то осуществляется переход на личную страницу товара на которой в будущем будет подробная характеристика товара

если кликнуть на кнопку "Положить в корзину" товар ложится в корзину и осуществляется переход на страницу корзины, а так же обновляется виджет корзины
![image](https://user-images.githubusercontent.com/83662114/192209254-ae17b339-4161-4bf2-acd8-52591ff7485b.png)
![image](https://user-images.githubusercontent.com/83662114/192209340-ee5834af-67a5-4114-9bd3-40e9dff2e8bf.png)

кнопка "Продолжить покупки" возвращает нас на ту страницу на который мы нажали кнопку "Положить в корзину" или иконку корзины

+ Страница редактирования товаров:
![image](https://user-images.githubusercontent.com/83662114/192210047-8e3c766f-197a-480c-a67e-c2d966aa3a20.png)

***(!!!форма сортировки слева пока не работает!!!)***

![image](https://user-images.githubusercontent.com/83662114/192210218-f45d0f41-cc71-4397-bf6e-9a36d0461c01.png)

если часть формы не заполнена, то появляется сообщения об ошибках подобные этому:
![image](https://user-images.githubusercontent.com/83662114/192210899-e26ace62-f224-4dac-af0d-46cc8075d87a.png)
*(в случае редактирования товара файл можно не загружать)*

+ Страница редактирования заказов
![image](https://user-images.githubusercontent.com/83662114/192211965-2cb0ea93-1be6-426b-99ac-b43b3d664dfc.png)

+ Страница редактирования пользователей
![image](https://user-images.githubusercontent.com/83662114/192212076-5b71cfca-2ae2-4338-b100-fc460448c8e6.png)

![image](https://user-images.githubusercontent.com/83662114/192212182-03edb70c-43ce-4a68-9129-d70bb633b206.png)

+ Страница оформления заказа
![image](https://user-images.githubusercontent.com/83662114/192212852-4ba5d3fb-fb24-489e-905b-b5e5bf2e15bb.png)

