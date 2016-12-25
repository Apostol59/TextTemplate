# TextTemplate
Шаблонизатор текстов.

Имеет зависимость от [IronPython 2.7.6.3](http://ironpython.net)

Основан на [handlebars](http://handlebarsjs.com) использован [порт на .net](https://github.com/rexm/Handlebars.Net)

В качестве основного синтаксиса шаблонов используется handlebars(далее по тексту hbs), со всеми его достоинствами. Но так как он не предполагает сложных проверок, добавлено расширение синтаксиса через python 2.7, на самом деле можно без труда подменить на ironruby или ironpython 3, ну или добавить их.

# Что делать?
1. На сервере ставим ironpython 2.7.6.3
2. Используем эту библиотечку на сервере в любой приложеньке, либо пишем wcf сервис, который занимается вопросом шаблонизации и использует эту библиотечку.

# Как работает?
Работает очень просто - ``` TextTemplate.Core.IHbsWrapperTemplate``` есть только один метод ```RenderTemplate(...)``` 

Внутри - чуть сложнее, происходит предварительная обработка шаблона - в тексте происходит поиск добавленных расширений, далее - замена расширений на некоторое значение в рамках синтаксиса hbs.

Так, например, в hbs есть **{{#if isA}}**Some text**{{else if isB}}**Other text**{{/if}}**, и если хочется сравнить что-то с чем-то, то такого функционала нет.

Добавляем. **{{#py.if name in ["A", "B", "C"]}}**Some text**{{#py.elif age > 18}}**Other text**{{/if}}**.


# Пример
```c#
            IHbsWrapperTemplate hbsWrapperTemplate = new HbsWrapperTemplate();
            var textResult = hbsWrapperTemplate.RenderTemplate(templateSource,parameters,partialSource);
```

# Параметры: 
1. ```templateSource``` - шаблон hbs, с расширенным синтаксисом, обязательный, остальные - опционально
2. ```parameters``` - словарь ```string - object```, так будут вводиться переменные в шаблон, ```string``` - имя переменной, ```object``` - значение
3. ```partialSource``` - словарь ```string - string```, если шаблон содержит вложенные шаблоны, то они указываются здесь имя вложенного шаблона - сам шаблон hbs

# Ограничения:
Python не лоялен к отсутствию переменных в отличии от hbs, если какой-то переменной нет - ошибка в рантайме.


# Если захотелось расширять:
## python 2.7 
Нужно реализовать ``` IPyHelper```, либо отнаследоваться от ``` BaseExtension<TResultExtension>``` 

можно найти в ``` TextTemplate.Core.PythonExtensions.PyExtensions.Base```

и добавить новый класс в ``` TextTemplate.Core.PythonExtensions.PythonHandler```

## other
реализовать ``` ITemplateHandler``` из ``` TextTemplate.Infrastructure``` 

добавить в ``` TextTemplate.Core.HbsWrapperTemplate```
