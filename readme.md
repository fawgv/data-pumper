[![Latest release](https://img.shields.io/github/v/release/fawgv/data-pumper)](https://github.com/fawgv/data-pumper/releases/latest)

Data Pumper
===========

Данная утилита позволяет переливать данные между таблицами/представлениями БД. Требование к таблицам - иметь колонку ActualData (дату актуальности записей) - на основании которых может осуществляться инкрементальное обновление данных.


Алгоритм работы следующий:

1. Для каждой таблицы утилита запоминает дату последней перезаливки (дата может переопределяться и быть не обязательно календарной, а браться из других таблиц)
2. В целевой таблице удаляются все записи, у которых ActualDate >= дате последней заливки.
3. Из таблицы-источника копируются все записи, у которых ActualDate >= дате последней заливки.
4. Обновляется дата успешной перезаливки.

На данный момент в качестве источника и целевой БД поддержкивается только Microsoft SQL Server (в качестве реализации используется SqlBulkCopy, наиболее эффективное решение для массовых ставок в SQL).


Установка
---------

Приложение можно запускать как консольное или установить как windows service командой
`DataPumper.Web.exe install --autostart`.

После установки панель управления будет доступна по адресу http://localhost:5000/
