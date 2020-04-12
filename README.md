NoSql/MongoDB + .Net/WPF (contain FRIENDS) technologies

В базі даних створила 2 колекції - Post i User.
Post містить OwnerId власника поста (тобто OwnerId посилається на _id користувача з колекції User).
Не створювала окремої колекції для коментарів, бо визначила це недоцільним, адже ніде окремо не витягуватиму їхній повний список.
Всі дані про коментарі до поста містяться безпосередньо в інформації про пост.

WPF-проект містить такі шари:
1.  Entity - набір класів, з якими я працюю в програмі (User, Post i Comment).
    В постах зберігаю список об'єктів Comment.
2.  DAL - доступ до бази даних в MongoDB.
    Набір класів для виконання операцій зчитування, запису, зміни та видалення даних з бази.
    Для кожної колекції в БД окремий робочий клас.
    ConnectionString до БД - в класі ConnectionManager.
3.  BLL - шар бізнес-логіки. 
    Містить класи для опрацювання функцій з DAL-рівня та інші потрібні для виконання вимог завдання функції.
4.  WPF - візуальна оболонка програми. 
    Складається з вікон:
        * логування (Login) з переходом до вікна реєстрації (Registration);
        * MainWindow з опціями переходу між UserControls:
                -   Timeline - аля-стрічка постів від людей, за якими користувач стежить.
                    Виконано у формі попередній/наступний. Пости в черзі від найновіших. Нові - позначені міткою NEW.
                -   AboutMe - сторінка користувача. Там він бачить всю інформації про себе і свої пости.
                -   Settings - доступна зміна власних даних чи зміна паролю.
                -   AboutFriend - сторінка іншого користувача (його інфа і всі пости).
                    Можна його зафоловити. Перехід до цієї сторінки можна здійснити лише через пошукову стрічку.
        * додаткові вікна для додавання і редагування постів, додавання коментарів і перегляду фоловерів, коментарів чи людей, які вподобали публікацію.
