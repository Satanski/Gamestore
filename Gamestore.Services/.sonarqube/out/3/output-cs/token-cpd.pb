�
dD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\BanHandler\BanService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

BanHandler "
;" #
public		 
class		 

BanService		 
:		 
IBanService		 %
{

 
private 
const 
string 
OneHourBanKeyWord *
=+ ,
$str- 5
;5 6
private 
const 
string 
OndeDayBanKeyWord *
=+ ,
$str- 4
;4 5
private 
const 
string 
OneWeekBanKeyword *
=+ ,
$str- 5
;5 6
private 
const 
string 
OneMonthBanKeyword +
=, -
$str. 7
;7 8
private 
const 
string 
PermanentBanKeyWord ,
=- .
$str/ :
;: ;
private 
readonly 

Dictionary 
<  
string  &
,& '
int( +
>+ ,
_banDuration- 9
=: ;
new< ?
(? @
)@ A
{ 
{ 	
OneHourBanKeyWord
 
, 
$num 
}  
,  !
{ 	
OndeDayBanKeyWord
 
, 
$num 
}  !
,! "
{ 	
OneWeekBanKeyword
 
, 
$num  
}! "
," #
{ 	
OneMonthBanKeyword
 
, 
$num !
}" #
,# $
{ 	
PermanentBanKeyWord
 
, 
$num &
}' (
,( )
} 
; 
public 

async 
Task *
BanCustomerFromCommentingAsync 4
(4 5
BanDto5 ;

banDetails< F
,F G
UserManagerH S
<S T
AppUserT [
>[ \
userManager] h
)h i
{ 
var 
u 
= 
await 
userManager !
.! "
Users" '
.' (
FirstOrDefaultAsync( ;
(; <
x< =
=>> @
xA B
.B C
UserNameC K
==L N

banDetailsO Y
.Y Z
UserZ ^
)^ _
;_ `
if 

( 
u 
is 
not 
null 
&& 
_banDuration )
.) *
TryGetValue* 5
(5 6

banDetails6 @
.@ A
DurationA I
,I J
outK N
intO R
durationS [
)[ \
)\ ]
{ 	
u 
. 

BannedTill 
= 
DateTime #
.# $
Now$ '
.' (
AddHours( 0
(0 1
duration1 9
)9 :
;: ;
await 
userManager 
. 
UpdateAsync )
() *
u* +
)+ ,
;, -
}   	
else!! 
{"" 	
throw## 
new## 
GamestoreException## (
(##( )
$str##) I
)##I J
;##J K
}$$ 	
}%% 
}&& �
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\BanHandler\IBanService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

BanHandler "
;" #
public 
	interface 
IBanService 
{ 
Task		 *
BanCustomerFromCommentingAsync			 '
(		' (
BanDto		( .

banDetails		/ 9
,		9 :
UserManager		; F
<		F G
AppUser		G N
>		N O
userManager		P [
)		[ \
;		\ ]
}

 �
yD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Configurations\PaymentServiceConfiguration.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Configurations &
;& '
public 
class '
PaymentServiceConfiguration (
{ 
public 

string 
VisaServiceUrl  
{! "
get# &
;& '
}( )
=* +
$str, U
;U V
public 

string 
IboxServiceUrl  
{! "
get# &
;& '
}( )
=* +
$str, U
;U V
} �*
jD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\DIRegistrations\BLLServices.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
DiRegistrations '
;' (
public 
static 
class 
BllServices 
{ 
public 

static 
void 
	Congigure  
(  !
IServiceCollection! 3
services4 <
)< =
{ 
services 
. 
	AddScoped 
< 
IGameService '
,' (
GameService) 4
>4 5
(5 6
)6 7
;7 8
services 
. 
	AddScoped 
< 
IPlatformService +
,+ ,
PlatformService- <
>< =
(= >
)> ?
;? @
services 
. 
	AddScoped 
< 
IGenreService (
,( )
GenreService* 6
>6 7
(7 8
)8 9
;9 :
services 
. 
	AddScoped 
< 
IPublisherService ,
,, -
PublisherService. >
>> ?
(? @
)@ A
;A B
services 
. 
	AddScoped 
< 
IOrderService (
,( )
OrderService* 6
>6 7
(7 8
)8 9
;9 :
services 
. 
	AddScoped 
< 
IShipperService *
,* +
ShipperService, :
>: ;
(; <
)< =
;= >
services 
. 
	AddScoped 
< 
IUserService '
,' (
UserService) 4
>4 5
(5 6
)6 7
;7 8
services 
. 
	AddScoped 
< 
IRoleService '
,' (
RoleService) 4
>4 5
(5 6
)6 7
;7 8
services 
. 
	AddScoped 
< '
PaymentServiceConfiguration 6
>6 7
(7 8
)8 9
;9 :
services 
. 
	AddScoped 
< 
ICommentService *
,* +
CommentService, :
>: ;
(; <
)< =
;= >
services 
. 
	AddScoped 
< 
IBanService &
,& '

BanService( 2
>2 3
(3 4
)4 5
;5 6
services 
. 
	AddScoped 
< 
GenreFilterHandler -
>- .
(. /
)/ 0
;0 1
services   
.   
	AddScoped   
<   
NameFilterHandler   ,
>  , -
(  - .
)  . /
;  / 0
services!! 
.!! 
	AddScoped!! 
<!! #
PaginationFilterHandler!! 2
>!!2 3
(!!3 4
)!!4 5
;!!5 6
services"" 
."" 
	AddScoped"" 
<"" !
PlatformFilterHandler"" 0
>""0 1
(""1 2
)""2 3
;""3 4
services## 
.## 
	AddScoped## 
<## 
PriceFilterHandler## -
>##- .
(##. /
)##/ 0
;##0 1
services$$ 
.$$ 
	AddScoped$$ 
<$$ 
PublishDateHandler$$ -
>$$- .
($$. /
)$$/ 0
;$$0 1
services%% 
.%% 
	AddScoped%% 
<%% "
PublisherFilterHandler%% 1
>%%1 2
(%%2 3
)%%3 4
;%%4 5
services&& 
.&& 
	AddScoped&& 
<&& 
SortingHandler&& )
>&&) *
(&&* +
)&&+ ,
;&&, -
services'' 
.'' 
	AddScoped'' 
<'' *
IGameProcessingPipelineBuilder'' 9
,''9 :)
GameProcessingPipelineBuilder''; X
>''X Y
(''Y Z
)''Z [
;''[ \
services(( 
.(( 
	AddScoped(( 
<(( +
IGameProcessingPipelineDirector(( :
,((: ;*
GameProcessingPipelineDirector((< Z
>((Z [
((([ \
)((\ ]
;((] ^
services)) 
.)) 
	AddScoped)) 
<))  
IMongoLoggingService)) /
,))/ 0
MongoLoggingService))1 D
>))D E
())E F
)))F G
;))G H
var++ #
autoMapperConfiguration++ #
=++$ %
new++& )
MapperConfiguration++* =
(++= >
m++> ?
=>++@ B
m++C D
.++D E

AddProfile++E O
(++O P
new++P S
MappingProfile++T b
(++b c
)++c d
)++d e
)++e f
;++f g
var,, 

autoMapper,, 
=,, #
autoMapperConfiguration,, 0
.,,0 1
CreateMapper,,1 =
(,,= >
),,> ?
;,,? @
services-- 
.-- 
AddSingleton-- 
(-- 

autoMapper-- (
)--( )
;--) *
}.. 
}// �&
`D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Documents\Invoice.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Documents !
;! "
public 
class 
Invoice 
( 
Order 
order  
,  !
double" (
amountToPay) 4
)4 5
:6 7
	IDocument8 A
{		 
public

 

void

 
Compose

 
(

 
IDocumentContainer

 *
	container

+ 4
)

4 5
{ 
var 
	validTill 
= 
order 
. 
	OrderDate '
.' (
AddDays( /
(/ 0
$num0 2
)2 3
;3 4
	container 
. 
Page 
( 
page 
=> 
{ 
page 
. 
Size 
( 
	PageSizes $
.$ %
A4% '
)' (
;( )
page 
. 
Margin 
( 
$num 
, 
Unit  $
.$ %

Centimetre% /
)/ 0
;0 1
page 
. 
	PageColor 
(  
Colors  &
.& '
White' ,
), -
;- .
page 
. 
DefaultTextStyle &
(& '
x' (
=>) +
x, -
.- .
FontSize. 6
(6 7
$num7 9
)9 :
): ;
;; <
page 
. 
Header 
( 
) 
. 
Text 
( 
$str $
)$ %
. 
SemiBold 
( 
)  
.  !
FontSize! )
() *
$num* ,
), -
.- .
	FontColor. 7
(7 8
Colors8 >
.> ?
Blue? C
.C D
MediumD J
)J K
. 
AlignCenter !
(! "
)" #
;# $
page 
. 
Content 
( 
) 
. 
PaddingVertical %
(% &
$num& '
,' (
Unit) -
.- .

Centimetre. 8
)8 9
. 
Column 
( 
column #
=>$ &
{ 
column 
.  
Item  $
($ %
)% &
.& '
Text' +
(+ ,
$", .
$str. 8
{8 9
order9 >
.> ?

CustomerId? I
}I J
"J K
)K L
;L M
column   
.    
Item    $
(  $ %
)  % &
.  & '
Text  ' +
(  + ,
$"  , .
$str  . 8
{  8 9
order  9 >
.  > ?
Id  ? A
}  A B
"  B C
)  C D
;  D E
column!! 
.!!  
Item!!  $
(!!$ %
)!!% &
.!!& '
Text!!' +
(!!+ ,
$"!!, .
$str!!. =
{!!= >
order!!> C
.!!C D
	OrderDate!!D M
}!!M N
"!!N O
)!!O P
;!!P Q
column"" 
.""  
Item""  $
(""$ %
)""% &
.""& '
Text""' +
(""+ ,
$""", .
$str"". :
{"": ;
	validTill""; D
:""D E
$str""E O
}""O P
"""P Q
)""Q R
;""R S
column## 
.##  
Item##  $
(##$ %
)##% &
.##& '
Text##' +
(##+ ,
$"##, .
$str##. =
{##= >
amountToPay##> I
}##I J
"##J K
)##K L
;##L M
}$$ 
)$$ 
;$$ 
page&& 
.&& 
Footer&& 
(&& 
)&& 
.'' 
AlignCenter'' !
(''! "
)''" #
.(( 
Text(( 
((( 
x(( 
=>(( 
{)) 
x** 
.** 
Span** 
(**  
$str**  '
)**' (
;**( )
x++ 
.++ 
CurrentPageNumber++ ,
(++, -
)++- .
;++. /
x,, 
.,, 
Span,, 
(,,  
$str,,  &
),,& '
;,,' (
x-- 
.-- 

TotalPages-- %
(--% &
)--& '
;--' (
}.. 
).. 
;.. 
}// 
)// 
;// 
}00 
}11 �
lD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Exceptions\GamestoreException.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Exceptions "
;" #
public 
class 
GamestoreException 
(  
string  &
message' .
). /
:0 1
	Exception2 ;
(; <
message< C
)C D
{ 
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\GameProcessingPipelineBuilder.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public 
class )
GameProcessingPipelineBuilder *
:+ ,*
IGameProcessingPipelineBuilder- K
{ 
private *
IGameProcessingPipelineHandler *
_firstHandler+ 8
;8 9
private *
IGameProcessingPipelineHandler *
_lastHandler+ 7
;7 8
public

 
*
IGameProcessingPipelineBuilder

 )

AddHandler

* 4
(

4 5*
IGameProcessingPipelineHandler

5 S
handler

T [
)

[ \
{ 
if 

( 
_firstHandler 
== 
null !
)! "
{ 	
_firstHandler 
= 
handler #
;# $
_lastHandler 
= 
handler "
;" #
} 	
else 
{ 	
_lastHandler 
. 
SetNext  
(  !
handler! (
)( )
;) *
_lastHandler 
= 
handler "
;" #
} 	
return 
this 
; 
} 
public 
*
IGameProcessingPipelineService )
Build* /
(/ 0
)0 1
{ 
return 
new )
GameProcessingPipelineService 0
(0 1
_firstHandler1 >
)> ?
;? @
} 
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\GameProcessingPipelineDirector.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public 
class *
GameProcessingPipelineDirector +
(+ ,*
IGameProcessingPipelineBuilder "
builder# *
,* +
GenreFilterHandler 
genreFilterHandler )
,) *
NameFilterHandler 
nameFilterHandler '
,' (#
PaginationFilterHandler		 #
paginationFilterHandler		 3
,		3 4!
PlatformFilterHandler

 !
platformFilterHandler

 /
,

/ 0
PriceFilterHandler 
priceFilterHandler )
,) *
PublishDateHandler 
publishDateHandler )
,) *"
PublisherFilterHandler "
publisherFilterHandler 1
,1 2
SortingHandler 
sortingHandler !
)! "
:# $+
IGameProcessingPipelineDirector% D
{ 
private 
readonly *
IGameProcessingPipelineBuilder 3
_builder4 <
== >
builder? F
;F G
public 
*
IGameProcessingPipelineService )2
&ConstructGameCollectionPipelineService* P
(P Q
)Q R
{ 
return 
_builder 
. 

AddHandler 
( 
genreFilterHandler *
)* +
. 

AddHandler 
( !
platformFilterHandler -
)- .
. 

AddHandler 
( "
publisherFilterHandler .
). /
. 

AddHandler 
( 
priceFilterHandler *
)* +
. 

AddHandler 
( 
publishDateHandler *
)* +
. 

AddHandler 
( 
nameFilterHandler )
)) *
. 

AddHandler 
( 
sortingHandler &
)& '
. 

AddHandler 
( #
paginationFilterHandler /
)/ 0
. 
Build 
( 
) 
; 
} 
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\GameProcessingPipelineHandlerBase.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public

 
class

 -
!GameProcessingPipelineHandlerBase

 .
:

/ 0*
IGameProcessingPipelineHandler

1 O
{ 
private *
IGameProcessingPipelineHandler *
_nextHandler+ 7
;7 8
public 

void 
SetNext 
( *
IGameProcessingPipelineHandler 6
nextHandler7 B
)B C
{ 
_nextHandler 
= 
nextHandler "
;" #
} 
public 

virtual 
async 
Task 
< 

IQueryable (
<( )
Game) -
>- .
>. /
HandleAsync0 ;
(; <
IUnitOfWork< G

unitOfWorkH R
,R S
IMongoUnitOfWorkT d
mongoUnitOfWorke t
,t u
GameFiltersDto	v �
filters
� �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
if 

( 
_nextHandler 
!= 
null  
)  !
{ 	
return 
await 
_nextHandler %
.% &
HandleAsync& 1
(1 2

unitOfWork2 <
,< =
mongoUnitOfWork> M
,M N
filtersO V
,V W
queryX ]
)] ^
;^ _
} 	
return 
query 
; 
} 
	protected 
static 
List 
< 
Guid 
> 
ConvertIdsToGuids  1
(1 2
List2 6
<6 7
int7 :
>: ;
ids< ?
)? @
{ 
List 
< 
Guid 
> 
guids 
= 
[ 
] 
; 
foreach!! 
(!! 
var!! 
id!! 
in!! 
ids!! 
)!! 
{"" 	
guids## 
.## 
Add## 
(## 
GuidHelpers## !
.##! "
	IntToGuid##" +
(##+ ,
id##, .
)##. /
)##/ 0
;##0 1
}$$ 	
return&& 
guids&& 
;&& 
}'' 
}(( �

�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\GameProcessingPipelineService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public		 
class		 )
GameProcessingPipelineService		 *
(		* +*
IGameProcessingPipelineHandler		+ I
handlerChain		J V
)		V W
:		X Y*
IGameProcessingPipelineService		Z x
{

 
public 

Task 
< 

IQueryable 
< 
Game 
>  
>  !
ProcessGamesAsync" 3
(3 4
IUnitOfWork4 ?

unitOfWork@ J
,J K
IMongoUnitOfWorkL \
mongoUnitOfWork] l
,l m
GameFiltersDton |
filters	} �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
return 
handlerChain 
. 
HandleAsync '
(' (

unitOfWork( 2
,2 3
mongoUnitOfWork4 C
,C D
filtersE L
,L M
queryN S
)S T
;T U
} 
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\GenreFilterHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public 
class 
GenreFilterHandler 
:  !-
!GameProcessingPipelineHandlerBase" C
{		 
public

 

override

 
async

 
Task

 
<

 

IQueryable

 )
<

) *
Game

* .
>

. /
>

/ 0
HandleAsync

1 <
(

< =
IUnitOfWork

= H

unitOfWork

I S
,

S T
IMongoUnitOfWork

U e
mongoUnitOfWork

f u
,

u v
GameFiltersDto	

w �
filters


� �
,


� �

IQueryable


� �
<


� �
Game


� �
>


� �
query


� �
)


� �
{ 
if 

( 
filters 
. 
Genres 
. 
Count  
!=! #
$num$ %
)% &
{ 	
query 
= 
query 
. 
Where 
(  
game  $
=>% '
game( ,
., -
ProductCategories- >
.> ?
Any? B
(B C
gpC E
=>F H
filtersI P
.P Q
GenresQ W
.W X
ContainsX `
(` a
gpa c
.c d
GenreIdd k
)k l
)l m
)m n
;n o
} 	
query 
= 
await 
base 
. 
HandleAsync &
(& '

unitOfWork' 1
,1 2
mongoUnitOfWork3 B
,B C
filtersD K
,K L
queryM R
)R S
;S T
return 
query 
; 
} 
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\NameFilterHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public		 
class		 
NameFilterHandler		 
:		  -
!GameProcessingPipelineHandlerBase		! B
{

 
public 

override 
async 
Task 
< 

IQueryable )
<) *
Game* .
>. /
>/ 0
HandleAsync1 <
(< =
IUnitOfWork= H

unitOfWorkI S
,S T
IMongoUnitOfWorkU e
mongoUnitOfWorkf u
,u v
GameFiltersDto	w �
filters
� �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
if 

( 
filters 
. 
Name 
is 
not 
null  $
)$ %
{ 	
if 
( 
filters 
. 
Name 
. 
Length #
<$ %
$num& '
)' (
{ 
throw 
new 
GamestoreException ,
(, -
$str- X
)X Y
;Y Z
} 
query 
= 
query 
. 
Where 
(  
x  !
=>" $
x% &
.& '
Name' +
.+ ,
Contains, 4
(4 5
filters5 <
.< =
Name= A
)A B
)B C
;C D
} 	
query 
= 
await 
base 
. 
HandleAsync &
(& '

unitOfWork' 1
,1 2
mongoUnitOfWork3 B
,B C
filtersD K
,K L
queryM R
)R S
;S T
return 
query 
; 
} 
} �5
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\PaginationFilterHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public 
class #
PaginationFilterHandler $
:% &-
!GameProcessingPipelineHandlerBase' H
{		 
private

 
readonly

 
string

 
	_allGames

 %
=

& ' 
PaginationOptionsDto

( <
.

< =
PaginationOptions

= N
[

N O
$num

O P
]

P Q
;

Q R
public 

override 
async 
Task 
< 

IQueryable )
<) *
Game* .
>. /
>/ 0
HandleAsync1 <
(< =
IUnitOfWork= H

unitOfWorkI S
,S T
IMongoUnitOfWorkU e
mongoUnitOfWorkf u
,u v
GameFiltersDto	w �
filters
� �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
var 
	pageCount 
= 
filters 
.  
	PageCount  )
;) *
switch 
( 
	pageCount 
) 
{ 	
case 
var 
filter 
when  
filter! '
==( *
	_allGames+ 4
:4 5
case 
null 
: 
filters 
. (
NumberOfPagesAfterFiltration 4
=5 6
$num7 8
;8 9
filters 
. +
NumberOfGamesFromPreviousSource 7
=8 9
query: ?
.? @
Count@ E
(E F
)F G
;G H
query 
= 
await 
base "
." #
HandleAsync# .
(. /

unitOfWork/ 9
,9 :
mongoUnitOfWork; J
,J K
filtersL S
,S T
queryU Z
)Z [
;[ \
return 
query 
; 
default 
: 
filters 
. (
NumberOfPagesAfterFiltration 4
=5 6-
!CountNumberOfPagesAfterFiltration7 X
(X Y
intY \
.\ ]
Parse] b
(b c
	pageCountc l
)l m
,m n
queryo t
,t u
filtersv }
)} ~
;~ 
var  
numberOfGamesPerPage (
=) *
int+ .
.. /
Parse/ 4
(4 5
	pageCount5 >
)> ?
;? @
int 
numberToSkip  
=! "*
CalculateNumberOfEntriesToSkip# A
(A B
filtersB I
,I J 
numberOfGamesPerPageK _
)_ `
;` a
filters 
. (
NumberOfPagesAfterFiltration 4
=5 6-
!CountNumberOfPagesAfterFiltration7 X
(X Y
intY \
.\ ]
Parse] b
(b c
	pageCountc l
)l m
,m n
queryo t
,t u
filtersv }
)} ~
;~ 
filters 
. +
NumberOfGamesFromPreviousSource 7
=8 9
query: ?
.? @
Count@ E
(E F
)F G
;G H
int 
numberToTake  
=! " 
numberOfGamesPerPage# 7
-8 9
filters: A
.A B4
(NumberOfDisplayedGamesFromPreviousSourceB j
;j k
query!! 
=!! 
query!! 
.!! 
Skip!! "
(!!" #
numberToSkip!!# /
)!!/ 0
.!!0 1
Take!!1 5
(!!5 6
numberToTake!!6 B
)!!B C
;!!C D
filters"" 
."" 4
(NumberOfDisplayedGamesFromPreviousSource"" @
=""A B
query""C H
.""H I
Count""I N
(""N O
)""O P
;""P Q
return$$ 
query$$ 
;$$ 
}%% 	
}&& 
private(( 
static(( 
int(( *
CalculateNumberOfEntriesToSkip(( 5
(((5 6
GameFiltersDto((6 D
filters((E L
,((L M
int((N Q 
numberOfGamesPerPage((R f
)((f g
{)) 
int** 
numberToSkip** 
;** 
if++ 

(++ 
filters++ 
.++ +
NumberOfGamesFromPreviousSource++ 3
==++4 6
$num++7 8
)++8 9
{,, 	
numberToSkip-- 
=--  
numberOfGamesPerPage-- /
*--0 1
(--2 3
filters--3 :
.--: ;
Page--; ?
---@ A
$num--B C
)--C D
;--D E
}.. 	
else// 
{00 	
numberToSkip11 
=11 
(11  
numberOfGamesPerPage11 0
*111 2
(113 4
filters114 ;
.11; <
Page11< @
-11A B
$num11C D
-11E F
(11G H
filters11H O
.11O P+
NumberOfGamesFromPreviousSource11P o
/11p q!
numberOfGamesPerPage	11r �
)
11� �
)
11� �
)
11� �
-
11� �
(
11� �
filters
11� �
.
11� �-
NumberOfGamesFromPreviousSource
11� �
%
11� �"
numberOfGamesPerPage
11� �
)
11� �
;
11� �
}22 	
return44 
numberToSkip44 
;44 
}55 
private77 
static77 
int77 -
!CountNumberOfPagesAfterFiltration77 8
(778 9
int779 < 
numberOfGamesPerPage77= Q
,77Q R

IQueryable77S ]
<77] ^
Game77^ b
>77b c
query77d i
,77i j
GameFiltersDto77k y
filters	77z �
)
77� �
{88 
var99 
	noOfGames99 
=99 
query99 
.99 
Count99 #
(99# $
)99$ %
+99& '
filters99( /
.99/ 0+
NumberOfGamesFromPreviousSource990 O
;99O P
return:: 
(:: 
int:: 
):: 
Math:: 
.:: 
Ceiling::  
(::  !
(::! "
double::" (
)::( )
	noOfGames::) 2
/::3 4 
numberOfGamesPerPage::5 I
)::I J
;::J K
};; 
}<< �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\PlatformFilterHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public 
class !
PlatformFilterHandler "
:# $-
!GameProcessingPipelineHandlerBase% F
{		 
public

 

override

 
async

 
Task

 
<

 

IQueryable

 )
<

) *
Game

* .
>

. /
>

/ 0
HandleAsync

1 <
(

< =
IUnitOfWork

= H

unitOfWork

I S
,

S T
IMongoUnitOfWork

U e
mongoUnitOfWork

f u
,

u v
GameFiltersDto	

w �
filters


� �
,


� �

IQueryable


� �
<


� �
Game


� �
>


� �
query


� �
)


� �
{ 
if 

( 
filters 
. 
	Platforms 
. 
Count #
!=$ &
$num' (
)( )
{ 	
query 
= 
query 
. 
Where 
(  
game  $
=>% '
game( ,
., -
ProductPlatforms- =
.= >
Any> A
(A B
gpB D
=>E G
filtersH O
.O P
	PlatformsP Y
.Y Z
ContainsZ b
(b c
gpc e
.e f

PlatformIdf p
)p q
)q r
)r s
;s t
} 	
query 
= 
await 
base 
. 
HandleAsync &
(& '

unitOfWork' 1
,1 2
mongoUnitOfWork3 B
,B C
filtersD K
,K L
queryM R
)R S
;S T
return 
query 
; 
} 
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\PriceFilterHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public		 
class		 
PriceFilterHandler		 
:		  !-
!GameProcessingPipelineHandlerBase		" C
{

 
public 

override 
async 
Task 
< 

IQueryable )
<) *
Game* .
>. /
>/ 0
HandleAsync1 <
(< =
IUnitOfWork= H

unitOfWorkI S
,S T
IMongoUnitOfWorkU e
mongoUnitOfWorkf u
,u v
GameFiltersDto	w �
filters
� �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
if 

( 
filters 
. 
MaxPrice 
< 
filters &
.& '
MinPrice' /
)/ 0
{ 	
throw 
new 
GamestoreException (
(( )
$str) T
)T U
;U V
} 	
if 

( 
filters 
. 
MinPrice 
!= 
null  $
)$ %
{ 	
query 
= 
query 
. 
Where 
(  
x  !
=>" $
x% &
.& '
Price' ,
>=- /
filters0 7
.7 8
MinPrice8 @
)@ A
;A B
} 	
if 

( 
filters 
. 
MaxPrice 
!= 
null  $
)$ %
{ 	
query 
= 
query 
. 
Where 
(  
x  !
=>" $
x% &
.& '
Price' ,
<=- /
filters0 7
.7 8
MaxPrice8 @
)@ A
;A B
} 	
query 
= 
await 
base 
. 
HandleAsync &
(& '

unitOfWork' 1
,1 2
mongoUnitOfWork3 B
,B C
filtersD K
,K L
queryM R
)R S
;S T
return 
query 
; 
} 
}   �.
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\PublishDateHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public		 
class		 
PublishDateHandler		 
:		  !-
!GameProcessingPipelineHandlerBase		" C
{

 
private 
readonly 
string 
	_lastWeek %
=& '!
PublishDateOptionsDto( =
.= >
PublishDateOptions> P
[P Q
$numQ R
]R S
;S T
private 
readonly 
string 

_lastMonth &
=' (!
PublishDateOptionsDto) >
.> ?
PublishDateOptions? Q
[Q R
$numR S
]S T
;T U
private 
readonly 
string 
	_lastYear %
=& '!
PublishDateOptionsDto( =
.= >
PublishDateOptions> P
[P Q
$numQ R
]R S
;S T
private 
readonly 
string 
	_twoYears %
=& '!
PublishDateOptionsDto( =
.= >
PublishDateOptions> P
[P Q
$numQ R
]R S
;S T
private 
readonly 
string 
_threeYears '
=( )!
PublishDateOptionsDto* ?
.? @
PublishDateOptions@ R
[R S
$numS T
]T U
;U V
public 

override 
async 
Task 
< 

IQueryable )
<) *
Game* .
>. /
>/ 0
HandleAsync1 <
(< =
IUnitOfWork= H

unitOfWorkI S
,S T
IMongoUnitOfWorkU e
mongoUnitOfWorkf u
,u v
GameFiltersDto	w �
filters
� �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
var 
publishingDate 
= 
filters $
.$ %
DatePublishing% 3
;3 4
var 
now 
= 
DateOnly 
. 
FromDateTime '
(' (
DateTime( 0
.0 1
Now1 4
)4 5
;5 6
switch 
( 
publishingDate 
) 
{ 	
case 
var 
filter 
when  
filter! '
==( *
	_lastWeek+ 4
:4 5
query 
= 
query 
. 
Where #
(# $
x$ %
=>& (
x) *
.* +
PublishDate+ 6
>=7 9
now: =
.= >
AddDays> E
(E F
-F G
$numG H
)H I
)I J
;J K
break 
; 
case 
var 
filter 
when  
filter! '
==( *

_lastMonth+ 5
:5 6
query 
= 
query 
. 
Where #
(# $
x$ %
=>& (
x) *
.* +
PublishDate+ 6
>=7 9
now: =
.= >
	AddMonths> G
(G H
-H I
$numI J
)J K
)K L
;L M
break 
; 
case   
var   
filter   
when    
filter  ! '
==  ( *
	_lastYear  + 4
:  4 5
query!! 
=!! 
query!! 
.!! 
Where!! #
(!!# $
x!!$ %
=>!!& (
x!!) *
.!!* +
PublishDate!!+ 6
>=!!7 9
now!!: =
.!!= >
AddYears!!> F
(!!F G
-!!G H
$num!!H I
)!!I J
)!!J K
;!!K L
break"" 
;"" 
case$$ 
var$$ 
filter$$ 
when$$  
filter$$! '
==$$( *
	_twoYears$$+ 4
:$$4 5
query%% 
=%% 
query%% 
.%% 
Where%% #
(%%# $
x%%$ %
=>%%& (
x%%) *
.%%* +
PublishDate%%+ 6
>=%%7 9
now%%: =
.%%= >
AddYears%%> F
(%%F G
-%%G H
$num%%H I
)%%I J
)%%J K
;%%K L
break&& 
;&& 
case(( 
var(( 
filter(( 
when((  
filter((! '
==((( *
_threeYears((+ 6
:((6 7
query)) 
=)) 
query)) 
.)) 
Where)) #
())# $
x))$ %
=>))& (
x))) *
.))* +
PublishDate))+ 6
>=))7 9
now)): =
.))= >
AddYears))> F
())F G
-))G H
$num))H I
)))I J
)))J K
;))K L
break** 
;** 
case,, 
null,, 
:,, 
break-- 
;-- 
default// 
:// 
throw00 
new00 
GamestoreException00 ,
(00, -
$str00- K
)00K L
;00L M
}11 	
query33 
=33 
await33 
base33 
.33 
HandleAsync33 &
(33& '

unitOfWork33' 1
,331 2
mongoUnitOfWork333 B
,33B C
filters33D K
,33K L
query33M R
)33R S
;33S T
return55 
query55 
;55 
}66 
}77 �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\PublisherFilterHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Handlers" *
;* +
public 
class "
PublisherFilterHandler #
:$ %-
!GameProcessingPipelineHandlerBase& G
{		 
public

 

override

 
async

 
Task

 
<

 

IQueryable

 )
<

) *
Game

* .
>

. /
>

/ 0
HandleAsync

1 <
(

< =
IUnitOfWork

= H

unitOfWork

I S
,

S T
IMongoUnitOfWork

U e
mongoUnitOfWork

f u
,

u v
GameFiltersDto	

w �
filters


� �
,


� �

IQueryable


� �
<


� �
Game


� �
>


� �
query


� �
)


� �
{ 
if 

( 
filters 
. 

Publishers 
. 
Count $
!=% '
$num( )
)) *
{ 	
query 
= 
query 
. 
Where 
(  
game  $
=>% '
filters( /
./ 0

Publishers0 :
.: ;
Contains; C
(C D
gameD H
.H I
	PublisherI R
.R S
IdS U
)U V
)V W
;W X
} 	
query 
= 
await 
base 
. 
HandleAsync &
(& '

unitOfWork' 1
,1 2
mongoUnitOfWork3 B
,B C
filtersD K
,K L
queryM R
)R S
;S T
return 
query 
; 
} 
} �'
}D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Handlers\SortingHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public		 
class		 
SortingHandler		 
:		 -
!GameProcessingPipelineHandlerBase		 ?
{

 
private 
readonly 
string 
_mostPoplar '
=( )
SortingOptionsDto* ;
.; <
SortingOptions< J
[J K
$numK L
]L M
;M N
private 
readonly 
string 
_mostCommented *
=+ ,
SortingOptionsDto- >
.> ?
SortingOptions? M
[M N
$numN O
]O P
;P Q
private 
readonly 
string 
	_priceAsc %
=& '
SortingOptionsDto( 9
.9 :
SortingOptions: H
[H I
$numI J
]J K
;K L
private 
readonly 
string 

_priceDesc &
=' (
SortingOptionsDto) :
.: ;
SortingOptions; I
[I J
$numJ K
]K L
;L M
private 
readonly 
string 
_new  
=! "
SortingOptionsDto# 4
.4 5
SortingOptions5 C
[C D
$numD E
]E F
;F G
public 

override 
async 
Task 
< 

IQueryable )
<) *
Game* .
>. /
>/ 0
HandleAsync1 <
(< =
IUnitOfWork= H

unitOfWorkI S
,S T
IMongoUnitOfWorkU e
mongoUnitOfWorkf u
,u v
GameFiltersDto	w �
filters
� �
,
� �

IQueryable
� �
<
� �
Game
� �
>
� �
query
� �
)
� �
{ 
string 

sortOption 
= 
filters #
.# $
Sort$ (
;( )
switch 
( 

sortOption 
) 
{ 	
case 
var 
filter 
when  
filter! '
==( *
_mostPoplar+ 6
:6 7
query 
= 
query 
. 
OrderByDescending /
(/ 0
x0 1
=>2 4
x5 6
.6 7
NumberOfViews7 D
)D E
;E F
break 
; 
case 
var 
filter 
when  
filter! '
==( *
_mostCommented+ 9
:9 :
query 
= 
query 
. 
OrderByDescending /
(/ 0
x0 1
=>2 4
x5 6
.6 7
Comments7 ?
.? @
Count@ E
)E F
;F G
break 
; 
case 
var 
filter 
when  
filter! '
==( *
	_priceAsc+ 4
:4 5
query 
= 
query 
. 
OrderBy %
(% &
x& '
=>( *
x+ ,
., -
Price- 2
)2 3
;3 4
break 
; 
case   
var   
filter   
when    
filter  ! '
==  ( *

_priceDesc  + 5
:  5 6
query!! 
=!! 
query!! 
.!! 
OrderByDescending!! /
(!!/ 0
x!!0 1
=>!!2 4
x!!5 6
.!!6 7
Price!!7 <
)!!< =
;!!= >
break"" 
;"" 
case## 
var## 
filter## 
when##  
filter##! '
==##( *
_new##+ /
:##/ 0
query$$ 
=$$ 
query$$ 
.$$ 
OrderByDescending$$ /
($$/ 0
x$$0 1
=>$$2 4
x$$5 6
.$$6 7
PublishDate$$7 B
)$$B C
;$$C D
break%% 
;%% 
case&& 
null&& 
:&& 
break'' 
;'' 
default(( 
:(( 
throw)) 
new)) 
GamestoreException)) ,
()), -
$str))- C
)))C D
;))D E
}** 	
query,, 
=,, 
await,, 
base,, 
.,, 
HandleAsync,, &
(,,& '

unitOfWork,,' 1
,,,1 2
mongoUnitOfWork,,3 B
,,,B C
filters,,D K
,,,K L
query,,M R
),,R S
;,,S T
return.. 
query.. 
;.. 
}// 
}00 �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\IGameProcessingPipelineBuilder.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public 
	interface *
IGameProcessingPipelineBuilder /
{ *
IGameProcessingPipelineBuilder "

AddHandler# -
(- .*
IGameProcessingPipelineHandler. L
handlerM T
)T U
;U V*
IGameProcessingPipelineService		 "
Build		# (
(		( )
)		) *
;		* +
}

 �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\IGameProcessingPipelineDirector.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public 
	interface +
IGameProcessingPipelineDirector 0
{ *
IGameProcessingPipelineService "2
&ConstructGameCollectionPipelineService# I
(I J
)J K
;K L
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\IGameProcessingPipelineHandler.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

BanHandler "
;" #
public 
	interface *
IGameProcessingPipelineHandler /
{		 
void

 
SetNext

	 
(

 *
IGameProcessingPipelineHandler

 /
nextHandler

0 ;
)

; <
;

< =
Task 
< 	

IQueryable	 
< 
Game 
> 
> 
HandleAsync &
(& '
IUnitOfWork' 2

unitOfWork3 =
,= >
IMongoUnitOfWork? O
mongoUnitOfWorkP _
,_ `
GameFiltersDtoa o
filtersp w
,w x

IQueryable	y �
<
� �
Game
� �
>
� �
query
� �
)
� �
;
� �
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\IGameProcessingPipelineService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
;! "
public 
	interface *
IGameProcessingPipelineService /
{		 
Task

 
<

 	

IQueryable

	 
<

 
Game

 
>

 
>

 
ProcessGamesAsync

 ,
(

, -
IUnitOfWork

- 8

unitOfWork

9 C
,

C D
IMongoUnitOfWork

E U
mongoUnitOfWork

V e
,

e f
GameFiltersDto

g u
filters

v }
,

} ~

IQueryable	

 �
<


� �
Game


� �
>


� �
query


� �
)


� �
;


� �
} �
}D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Models\FilteredGamesDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Models" (
;( )
public 
class 
FilteredGamesDto 
{ 
public 

List 
< 
GameModelDto 
> 
Games #
{$ %
get& )
;) *
set+ .
;. /
}0 1
=2 3
[4 5
]5 6
;6 7
public		 

int		 
?		 

TotalPages		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public 

int 
CurrentPage 
{ 
get  
;  !
set" %
;% &
}' (
} �
{D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Models\GameFiltersDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Models" (
;( )
public 
class 
GameFiltersDto 
{ 
public 

List 
< 
Guid 
> 
Genres 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
[- .
]. /
;/ 0
public		 

List		 
<		 
Guid		 
>		 
	Platforms		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
=		. /
[		0 1
]		1 2
;		2 3
public 

List 
< 
Guid 
> 

Publishers  
{! "
get# &
;& '
set( +
;+ ,
}- .
=/ 0
[1 2
]2 3
;3 4
public 

int 
Page 
{ 
get 
; 
set 
; 
}  !
=" #
$num$ %
;% &
public 

int 
? 
MinPrice 
{ 
get 
; 
set  #
;# $
}% &
public 

int 
? 
MaxPrice 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
? 
DatePublishing !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

string 
? 
Sort 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
? 
	PageCount 
{ 
get "
;" #
set$ '
;' (
}) *
public 

string 
? 
Name 
{ 
get 
; 
set "
;" #
}$ %
[ 

JsonIgnore 
] 
public 

int (
NumberOfPagesAfterFiltration +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
=: ;
$num< =
;= >
[ 

JsonIgnore 
] 
public 

int +
NumberOfGamesFromPreviousSource .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
== >
$num? @
;@ A
[!! 

JsonIgnore!! 
]!! 
public"" 

int"" 4
(NumberOfDisplayedGamesFromPreviousSource"" 7
{""8 9
get"": =
;""= >
internal""? G
set""H K
;""K L
}""M N
}## �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Models\PaginationOptionsDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Models" (
;( )
public 
static 
class  
PaginationOptionsDto (
{ 
public 

static 
List 
< 
string 
> 
PaginationOptions 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
=? @
[A B
$strB F
,F G
$strH L
,L M
$strN R
,R S
$strT Y
,Y Z
$str[ `
]` a
;a b
} �
�D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Models\PublishDateOptionsDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Models" (
;( )
public 
static 
class !
PublishDateOptionsDto )
{ 
public 

static 
List 
< 
string 
> 
PublishDateOptions 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
=@ A
[B C
$strC N
,N O
$strP \
,\ ]
$str^ i
,i j
$strk t
,t u
$strv 
]	 �
;
� �
} �
~D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\GameProcessingPipeline\Models\SortingOptionsDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
	Filtering !
.! "
Models" (
;( )
public 
static 
class 
SortingOptionsDto %
{ 
public 

static 
List 
< 
string 
> 
SortingOptions -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
=< =
[> ?
$str? M
,M N
$strO _
,_ `
$stra l
,l m
$strn z
,z {
$str	| �
]
� �
;
� �
} �
pD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Helpers\AutoGenrateGameKeyHelpers.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Helpers 
;  
internal 
static	 
class %
AutoGenrateGameKeyHelpers /
{ 
internal 
static 
string 
GenerateGameKey *
(* +
string+ 1
name2 6
)6 7
{ 
return 
name 
; 
} 
}		 �1
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Helpers\CommentHelpers.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Helpers 
;  
internal 
class	 
CommentHelpers 
( 
IMapper %

automapper& 0
)0 1
{ 
private		 
const		 
string		 
QuoteTemplateString		 ,
=		- .
$str		/ :
;		: ;
internal 
List 
< 
CommentModel 
> 
CommentListCreator  2
(2 3
List3 7
<7 8
Comment8 ?
>? @
commentsA I
)I J
{ 
List 
< 
CommentModel 
> 
commentList &
=' (
[) *
]* +
;+ ,
foreach 
( 
var 
comment 
in 
comments  (
)( )
{ 	
if 
( 
comment 
. 
ParentCommentId '
!=( *
null+ /
)/ 0
{ 
FindParentComment !
(! "
comment" )
,) *
commentList+ 6
,6 7
comment8 ?
.? @
ParentCommentId@ O
)O P
;P Q
} 
else 
{ 
commentList 
. 
Add 
(  

automapper  *
.* +
Map+ .
<. /
CommentModel/ ;
>; <
(< =
comment= D
)D E
)E F
;F G
} 
} 	
return 
commentList 
; 
} 
private 
bool 
FindParentComment "
(" #
Comment# *
comment+ 2
,2 3
List4 8
<8 9
CommentModel9 E
>E F
commentListG R
,R S
GuidT X
?X Y
parentCommentIdZ i
)i j
{ 
var 
parentComment 
= 
commentList '
.' (
Find( ,
(, -
x- .
=>/ 1
x2 3
.3 4
Id4 6
==7 9
parentCommentId: I
)I J
;J K
if   

(   
parentComment   
!=   
null   !
)  ! "
{!! 	
var"" 
commentModel"" 
="" 
ComposeComment"" -
(""- .

automapper"". 8
,""8 9
comment"": A
,""A B
parentComment""C P
)""P Q
;""Q R
parentComment## 
.## 
ChildComments## '
.##' (
Add##( +
(##+ ,
commentModel##, 8
)##8 9
;##9 :
return$$ 
true$$ 
;$$ 
}%% 	
else&& 
{'' 	
foreach(( 
((( 
var(( 
com(( 
in(( 
(((  !
from((! %
com((& )
in((* ,
commentList((- 8
from))! %
c))& '
in))( *
com))+ .
.)). /
ChildComments))/ <
select**! '
com**( +
)**+ ,
.**, -
Distinct**- 5
(**5 6
)**6 7
)**7 8
{++ 
var,, 
isFound,, 
=,, 
FindParentComment,, /
(,,/ 0
comment,,0 7
,,,7 8
com,,9 <
.,,< =
ChildComments,,= J
,,,J K
parentCommentId,,L [
),,[ \
;,,\ ]
if-- 
(-- 
isFound-- 
)-- 
{.. 
break// 
;// 
}00 
}11 
}22 	
return44 
false44 
;44 
}55 
private77 
static77 
CommentModel77 
ComposeComment77  .
(77. /
IMapper77/ 6

automapper777 A
,77A B
Comment77C J
comment77K R
,77R S
CommentModel77T `
?77` a
parentComment77b o
)77o p
{88 
var99 
commentModel99 
=99 

automapper99 %
.99% &
Map99& )
<99) *
CommentModel99* 6
>996 7
(997 8
comment998 ?
)99? @
;99@ A
if;; 

(;; 
commentModel;; 
.;; 
Body;; 
.;; 
Contains;; &
(;;& '
QuoteTemplateString;;' :
,;;: ;
StringComparison;;< L
.;;L M&
InvariantCultureIgnoreCase;;M g
);;g h
);;h i
{<< 	
commentModel== 
.== 
Body== 
=== 
commentModel==  ,
.==, -
Body==- 1
.==1 2
Replace==2 9
(==9 :
QuoteTemplateString==: M
,==M N
string==O U
.==U V
Empty==V [
)==[ \
;==\ ]
commentModel>> 
.>> 
Body>> 
=>> 
commentModel>>  ,
.>>, -
Body>>- 1
.>>1 2
Insert>>2 8
(>>8 9
$num>>9 :
,>>: ;
$">>< >
$str>>> I
{>>I J
parentComment>>J W
.>>W X
Name>>X \
}>>\ ]
$str>>] `
{>>` a
parentComment>>a n
.>>n o
Body>>o s
}>>s t
$str>>t x
">>x y
)>>y z
;>>z {
}?? 	
else@@ 
{AA 	
commentModelBB 
.BB 
BodyBB 
=BB 
commentModelBB  ,
.BB, -
BodyBB- 1
.BB1 2
InsertBB2 8
(BB8 9
$numBB9 :
,BB: ;
$"BB< >
$strBB> I
{BBI J
parentCommentBBJ W
.BBW X
NameBBX \
}BB\ ]
$strBB] _
"BB_ `
)BB` a
;BBa b
}CC 	
returnEE 
commentModelEE 
;EE 
}FF 
}GG ��
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Helpers\MappingProfile.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Helpers 
;  
public 
class 
MappingProfile 
: 
Profile %
{ 
public 

MappingProfile 
( 
) 
{ 
	CreateMap 
< 

GameGenres 
, 
Genre #
># $
($ %
)% &
.	 

	ForMember
 
( 
dst 
=> 
dst 
. 
Name #
,# $
src% (
=>) +
src, /
./ 0
MapFrom0 7
(7 8
x8 9
=>: <
x= >
.> ?
Category? G
.G H
NameH L
)L M
)M N
.	 

	ForMember
 
( 
dst 
=> 
dst 
. 
Id !
,! "
src# &
=>' )
src* -
.- .
MapFrom. 5
(5 6
x6 7
=>8 :
x; <
.< =
GenreId= D
)D E
)E F
;F G
	CreateMap 
< 

GameGenres 
, 
GenreModelDto +
>+ ,
(, -
)- .
. 
	ForMember 
( 
dest 
=> 
dest "
." #
Id# %
,% &
src' *
=>+ -
src. 1
.1 2
MapFrom2 9
(9 :
x: ;
=>< >
x? @
.@ A
CategoryA I
.I J
IdJ L
)L M
)M N
. 
	ForMember 
( 
dest 
=> 
dest "
." #
Name# '
,' (
src) ,
=>- /
src0 3
.3 4
MapFrom4 ;
(; <
x< =
=>> @
xA B
.B C
CategoryC K
.K L
NameL P
)P Q
)Q R
. 

ReverseMap 
( 
) 
; 
	CreateMap 
< 
GamePlatform 
, 
Platform  (
>( )
() *
)* +
. 
	ForMember 
( 
dst 
=> 
dst !
.! "
Id" $
,$ %
src& )
=>* ,
src- 0
.0 1
MapFrom1 8
(8 9
x9 :
=>; =
x> ?
.? @
Platform@ H
.H I
IdI K
)K L
)L M
. 
	ForMember 
( 
dst 
=> 
dst !
.! "
Type" &
,& '
src( +
=>, .
src/ 2
.2 3
MapFrom3 :
(: ;
x; <
=>= ?
x@ A
.A B
PlatformB J
.J K
TypeK O
)O P
)P Q
;Q R
	CreateMap 
< 
GamePlatform 
, 
PlatformModelDto  0
>0 1
(1 2
)2 3
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Id$ &
,& '
src( +
=>, .
src/ 2
.2 3
MapFrom3 :
(: ;
x; <
=>= ?
x@ A
.A B
PlatformB J
.J K
IdK M
)M N
)N O
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Type$ (
,( )
src* -
=>. 0
src1 4
.4 5
MapFrom5 <
(< =
x= >
=>? A
xB C
.C D
PlatformD L
.L M
TypeM Q
)Q R
)R S
.   

ReverseMap   
(   
)   
;   
	CreateMap"" 
<"" 
Platform"" 
,"" 
PlatformModelDto"" ,
>"", -
(""- .
)"". /
.""/ 0

ReverseMap""0 :
("": ;
)""; <
;""< =
	CreateMap## 
<## 
Genre## 
,## 
GenreModelDto## &
>##& '
(##' (
)##( )
.##) *

ReverseMap##* 4
(##4 5
)##5 6
;##6 7
	CreateMap$$ 
<$$ 
	Publisher$$ 
,$$ 
PublisherModelDto$$ .
>$$. /
($$/ 0
)$$0 1
.$$1 2

ReverseMap$$2 <
($$< =
)$$= >
;$$> ?
	CreateMap%% 
<%% 
Order%% 
,%% 
OrderModelDto%% &
>%%& '
(%%' (
)%%( )
.&& 
	ForMember&& 
(&& 
dest&& 
=>&& 
dest&& #
.&&# $
Date&&$ (
,&&( )
src&&* -
=>&&. 0
src&&1 4
.&&4 5
MapFrom&&5 <
(&&< =
x&&= >
=>&&? A
x&&B C
.&&C D
	OrderDate&&D M
.&&M N
ToString&&N V
(&&V W
$str&&W c
)&&c d
)&&d e
)&&e f
.'' 

ReverseMap'' 
('' 
)'' 
;'' 
	CreateMap)) 
<)) 
	OrderGame)) 
,)) 
OrderGameModelDto)) .
>)). /
())/ 0
)))0 1
.))1 2

ReverseMap))2 <
())< =
)))= >
;))> ?
	CreateMap** 
<** 
	OrderGame** 
,** 
OrderDetailsDto** ,
>**, -
(**- .
)**. /
.++ 
	ForMember++ 
(++ 
dest++ 
=>++ 
dest++ #
.++# $
	ProductId++$ -
,++- .
src++/ 2
=>++3 5
src++6 9
.++9 :
MapFrom++: A
(++A B
x++B C
=>++D F
x++G H
.++H I
GameId++I O
)++O P
)++P Q
;++Q R
	CreateMap-- 
<-- 
Game-- 
,-- 
GameModelDto-- $
>--$ %
(--% &
)--& '
... 
	ForMember.. 
(.. 
dest.. 
=>.. 
dest.. #
...# $
Id..$ &
,..& '
src..( +
=>.., .
src../ 2
...2 3
MapFrom..3 :
(..: ;
x..; <
=>..= ?
x..@ A
...A B
Id..B D
)..D E
)..E F
.// 
	ForMember// 
(// 
dest// 
=>// 
dest// #
.//# $
Description//$ /
,/// 0
src//1 4
=>//5 7
src//8 ;
.//; <
MapFrom//< C
<//C D
string//D J
>//J K
(//K L
x//L M
=>//N P
x//Q R
.//R S
Description//S ^
)//^ _
)//_ `
.00 
	ForMember00 
(00 
dest00 
=>00 
dest00 #
.00# $
Price00$ )
,00) *
src00+ .
=>00/ 1
src002 5
.005 6
MapFrom006 =
(00= >
x00> ?
=>00@ B
x00C D
.00D E
Price00E J
)00J K
)00K L
.11 
	ForMember11 
(11 
dest11 
=>11 
dest11 #
.11# $
PublishDate11$ /
,11/ 0
src111 4
=>115 7
src118 ;
.11; <
MapFrom11< C
(11C D
x11D E
=>11F H
x11I J
.11J K
PublishDate11K V
)11V W
)11W X
.22 
	ForMember22 
<22 
PublisherModelDto22 (
>22( )
(22) *
dest22* .
=>22/ 1
dest222 6
.226 7
	Publisher227 @
,22@ A
src22B E
=>22F H
src22I L
.22L M
MapFrom22M T
(22T U
x22U V
=>22W Y
x22Z [
.22[ \
	Publisher22\ e
)22e f
)22f g
.33 
	ForMember33 
(33 
dest33 
=>33 
dest33 #
.33# $
Discontinued33$ 0
,330 1
src332 5
=>336 8
src339 <
.33< =
MapFrom33= D
(33D E
x33E F
=>33G I
x33J K
.33K L
Discount33L T
)33T U
)33U V
.44 
	ForMember44 
<44 
List44 
<44 
PlatformModelDto44 ,
>44, -
>44- .
(44. /
dest44/ 3
=>444 6
dest447 ;
.44; <
	Platforms44< E
,44E F
src44G J
=>44K M
src44N Q
.44Q R
MapFrom44R Y
(44Y Z
x44Z [
=>44\ ^
x44_ `
.44` a
ProductPlatforms44a q
)44q r
)44r s
.55 
	ForMember55 
<55 
List55 
<55 
GenreModelDto55 )
>55) *
>55* +
(55+ ,
dest55, 0
=>551 3
dest554 8
.558 9
Genres559 ?
,55? @
src55A D
=>55E G
src55H K
.55K L
MapFrom55L S
(55S T
x55T U
=>55V X
x55Y Z
.55Z [
ProductCategories55[ l
)55l m
)55m n
;55n o
	CreateMap77 
<77 
GameModelDto77 
,77 
Game77  $
>77$ %
(77% &
)77& '
.88 
	ForMember88 
(88 
dest88 
=>88 
dest88 #
.88# $
Id88$ &
,88& '
src88( +
=>88, .
src88/ 2
.882 3
MapFrom883 :
(88: ;
x88; <
=>88= ?
x88@ A
.88A B
Id88B D
)88D E
)88E F
.99 
	ForMember99 
<99 
string99 
>99 
(99 
dest99 #
=>99$ &
dest99' +
.99+ ,
Description99, 7
,997 8
src999 <
=>99= ?
src99@ C
.99C D
MapFrom99D K
(99K L
x99L M
=>99N P
x99Q R
.99R S
Description99S ^
)99^ _
)99_ `
.:: 
	ForMember:: 
(:: 
dest:: 
=>:: 
dest:: #
.::# $
Price::$ )
,::) *
src::+ .
=>::/ 1
src::2 5
.::5 6
MapFrom::6 =
(::= >
x::> ?
=>::@ B
x::C D
.::D E
Price::E J
)::J K
)::K L
.;; 
	ForMember;; 
(;; 
dest;; 
=>;; 
dest;; #
.;;# $
PublishDate;;$ /
,;;/ 0
src;;1 4
=>;;5 7
src;;8 ;
.;;; <
MapFrom;;< C
(;;C D
x;;D E
=>;;F H
x;;I J
.;;J K
PublishDate;;K V
);;V W
);;W X
.<< 
	ForMember<< 
(<< 
dest<< 
=><< 
dest<< #
.<<# $
	Publisher<<$ -
,<<- .
src<</ 2
=><<3 5
src<<6 9
.<<9 :
MapFrom<<: A
<<<A B
PublisherModelDto<<B S
><<S T
(<<T U
x<<U V
=><<W Y
x<<Z [
.<<[ \
	Publisher<<\ e
)<<e f
)<<f g
.== 
	ForMember== 
(== 
dest== 
=>== 
dest== #
.==# $
Discount==$ ,
,==, -
src==. 1
=>==2 4
src==5 8
.==8 9
MapFrom==9 @
(==@ A
x==A B
=>==C E
x==F G
.==G H
Discontinued==H T
)==T U
)==U V
.>> 
	ForMember>> 
(>> 
dest>> 
=>>> 
dest>> #
.>># $
ProductPlatforms>>$ 4
,>>4 5
src>>6 9
=>>>: <
src>>= @
.>>@ A
Ignore>>A G
(>>G H
)>>H I
)>>I J
.?? 
	ForMember?? 
(?? 
dest?? 
=>?? 
dest?? #
.??# $
ProductCategories??$ 5
,??5 6
src??7 :
=>??; =
src??> A
.??A B
Ignore??B H
(??H I
)??I J
)??J K
;??K L
	CreateMapAA 
<AA 
PaymentModelDtoAA !
,AA! "(
VisaMicroservicePaymentModelAA# ?
>AA? @
(AA@ A
)AAA B
.BB 
	ForMemberBB 
(BB 
destBB 
=>BB 
destBB #
.BB# $
CardHolderNameBB$ 2
,BB2 3
srcBB4 7
=>BB8 :
srcBB; >
.BB> ?
MapFromBB? F
(BBF G
xBBG H
=>BBI K
xBBL M
.BBM N
ModelBBN S
.BBS T
HolderBBT Z
)BBZ [
)BB[ \
.CC 
	ForMemberCC 
(CC 
destCC 
=>CC 
destCC #
.CC# $

CardNumberCC$ .
,CC. /
srcCC0 3
=>CC4 6
srcCC7 :
.CC: ;
MapFromCC; B
(CCB C
xCCC D
=>CCE G
xCCH I
.CCI J
ModelCCJ O
.CCO P

CardNumberCCP Z
)CCZ [
)CC[ \
.DD 
	ForMemberDD 
(DD 
destDD 
=>DD 
destDD #
.DD# $
CvvDD$ '
,DD' (
srcDD) ,
=>DD- /
srcDD0 3
.DD3 4
MapFromDD4 ;
(DD; <
xDD< =
=>DD> @
xDDA B
.DDB C
ModelDDC H
.DDH I
Cvv2DDI M
)DDM N
)DDN O
.EE 
	ForMemberEE 
(EE 
destEE 
=>EE 
destEE #
.EE# $
ExpirationMonthEE$ 3
,EE3 4
srcEE5 8
=>EE9 ;
srcEE< ?
.EE? @
MapFromEE@ G
(EEG H
xEEH I
=>EEJ L
xEEM N
.EEN O
ModelEEO T
.EET U
MonthExpireEEU `
)EE` a
)EEa b
.FF 
	ForMemberFF 
(FF 
destFF 
=>FF 
destFF #
.FF# $
ExpirationYearFF$ 2
,FF2 3
srcFF4 7
=>FF8 :
srcFF; >
.FF> ?
MapFromFF? F
(FFF G
xFFG H
=>FFI K
xFFL M
.FFM N
ModelFFN S
.FFS T

YearExpireFFT ^
)FF^ _
)FF_ `
.GG 
	ForMemberGG 
(GG 
destGG 
=>GG 
destGG #
.GG# $
TransactionAmountGG$ 5
,GG5 6
srcGG7 :
=>GG; =
srcGG> A
.GGA B
MapFromGGB I
(GGI J
xGGJ K
=>GGL N
xGGO P
.GGP Q
ModelGGQ V
.GGV W
TransactionAmountGGW h
)GGh i
)GGi j
.HH 

ReverseMapHH 
(HH 
)HH 
;HH 
	CreateMapJJ 
<JJ 
CommentJJ 
,JJ 
CommentModelJJ '
>JJ' (
(JJ( )
)JJ) *
.KK 
	ForMemberKK 
(KK 
destKK 
=>KK 
destKK #
.KK# $
IdKK$ &
,KK& '
srcKK( +
=>KK, .
srcKK/ 2
.KK2 3
MapFromKK3 :
(KK: ;
xKK; <
=>KK= ?
xKK@ A
.KKA B
IdKKB D
)KKD E
)KKE F
.LL 
	ForMemberLL 
(LL 
destLL 
=>LL 
destLL #
.LL# $
NameLL$ (
,LL( )
srcLL* -
=>LL. 0
srcLL1 4
.LL4 5
MapFromLL5 <
(LL< =
xLL= >
=>LL? A
xLLB C
.LLC D
NameLLD H
)LLH I
)LLI J
.MM 
	ForMemberMM 
(MM 
destMM 
=>MM 
destMM #
.MM# $
BodyMM$ (
,MM( )
srcMM* -
=>MM. 0
srcMM1 4
.MM4 5
MapFromMM5 <
(MM< =
xMM= >
=>MM? A
xMMB C
.MMC D
BodyMMD H
)MMH I
)MMI J
;MMJ K
	CreateMapOO 
<OO 
MongoProductOO 
,OO 
GameOO  $
>OO$ %
(OO% &
)OO& '
.PP 
	ForMemberPP 
(PP 
destPP 
=>PP 
destPP "
.PP" #
NamePP# '
,PP' (
srcPP) ,
=>PP- /
srcPP0 3
.PP3 4
MapFromPP4 ;
(PP; <
xPP< =
=>PP> @
xPPA B
.PPB C
ProductNamePPC N
)PPN O
)PPO P
.QQ 
	ForMemberQQ 
(QQ 
destQQ 
=>QQ 
destQQ "
.QQ" #
KeyQQ# &
,QQ& '
srcQQ( +
=>QQ, .
srcQQ/ 2
.QQ2 3
MapFromQQ3 :
(QQ: ;
xQQ; <
=>QQ= ?
xQQ@ A
.QQA B
ProductNameQQB M
)QQM N
)QQN O
.RR 
	ForMemberRR 
(RR 
destRR 
=>RR 
destRR "
.RR" #
IdRR# %
,RR% &
srcRR' *
=>RR+ -
srcRR. 1
.RR1 2
MapFromRR2 9
(RR9 :
xRR: ;
=>RR< >
xRR? @
.RR@ A
ProductIdGuidRRA N
)RRN O
)RRO P
.SS 
	ForMemberSS 
(SS 
destSS 
=>SS 
destSS "
.SS" #
PriceSS# (
,SS( )
srcSS* -
=>SS. 0
srcSS1 4
.SS4 5
MapFromSS5 <
(SS< =
xSS= >
=>SS? A
xSSB C
.SSC D
	UnitPriceSSD M
)SSM N
)SSN O
.TT 
	ForMemberTT 
(TT 
destTT 
=>TT 
destTT "
.TT" #
UnitInStockTT# .
,TT. /
srcTT0 3
=>TT4 6
srcTT7 :
.TT: ;
MapFromTT; B
(TTB C
xTTC D
=>TTE G
xTTH I
.TTI J
UnitsInStockTTJ V
)TTV W
)TTW X
.UU 
	ForMemberUU 
(UU 
destUU 
=>UU 
destUU "
.UU" #
DiscountUU# +
,UU+ ,
srcUU- 0
=>UU1 3
srcUU4 7
.UU7 8
MapFromUU8 ?
(UU? @
xUU@ A
=>UUB D
xUUE F
.UUF G
DiscontinuedUUG S
)UUS T
)UUT U
.VV 
	ForMemberVV 
(VV 
destVV 
=>VV 
destVV "
.VV" #
DescriptionVV# .
,VV. /
srcVV0 3
=>VV4 6
srcVV7 :
.VV: ;
MapFromVV; B
(VVB C
xVVC D
=>VVE G
xVVH I
.VVI J
QuantityPerUnitVVJ Y
)VVY Z
)VVZ [
.WW 
	ForMemberWW 
(WW 
destWW 
=>WW 
destWW "
.WW" #
	PublisherWW# ,
,WW, -
srcWW. 1
=>WW2 4
srcWW5 8
.WW8 9
MapFromWW9 @
(WW@ A
xWWA B
=>WWC E
xWWF G
.WWG H
SupplierWWH P
)WWP Q
)WWQ R
.XX 
	ForMemberXX 
(XX 
destXX 
=>XX 
destXX "
.XX" #
ProductCategoriesXX# 4
,XX4 5
srcXX6 9
=>XX: <
srcXX= @
.XX@ A
MapFromXXA H
(XXH I
xXXI J
=>XXK M
xXXN O
.XXO P
ProductGenresXXP ]
)XX] ^
)XX^ _
.YY 
	ForMemberYY 
(YY 
destYY 
=>YY 
destYY "
.YY" #
CommentsYY# +
,YY+ ,
srcYY- 0
=>YY1 3
srcYY4 7
.YY7 8
MapFromYY8 ?
(YY? @
xYY@ A
=>YYB D
newYYE H
ListYYI M
<YYM N
CommentYYN U
>YYU V
(YYV W
)YYW X
)YYX Y
)YYY Z
;YYZ [
	CreateMap[[ 
<[[ 
MongoProduct[[ 
,[[ 
GameModelDto[[  ,
>[[, -
([[- .
)[[. /
.\\ 
	ForMember\\ 
(\\ 
dest\\ 
=>\\ 
dest\\ #
.\\# $
Name\\$ (
,\\( )
src\\* -
=>\\. 0
src\\1 4
.\\4 5
MapFrom\\5 <
(\\< =
x\\= >
=>\\? A
x\\B C
.\\C D
ProductName\\D O
)\\O P
)\\P Q
.]] 
	ForMember]] 
<]] 
string]] 
>]] 
(]] 
dest]] #
=>]]$ &
dest]]' +
.]]+ ,
Key]], /
,]]/ 0
src]]1 4
=>]]5 7
src]]8 ;
.]]; <
MapFrom]]< C
(]]C D
x]]D E
=>]]F H
x]]I J
.]]J K
ProductName]]K V
)]]V W
)]]W X
.^^ 
	ForMember^^ 
(^^ 
dest^^ 
=>^^ 
dest^^ #
.^^# $
Id^^$ &
,^^& '
src^^( +
=>^^, .
src^^/ 2
.^^2 3
MapFrom^^3 :
(^^: ;
x^^; <
=>^^= ?
x^^@ A
.^^A B
ProductIdGuid^^B O
)^^O P
)^^P Q
.__ 
	ForMember__ 
(__ 
dest__ 
=>__ 
dest__ #
.__# $
Price__$ )
,__) *
src__+ .
=>__/ 1
src__2 5
.__5 6
MapFrom__6 =
(__= >
x__> ?
=>__@ B
x__C D
.__D E
	UnitPrice__E N
)__N O
)__O P
.`` 
	ForMember`` 
(`` 
dest`` 
=>`` 
dest`` #
.``# $
UnitInStock``$ /
,``/ 0
src``1 4
=>``5 7
src``8 ;
.``; <
MapFrom``< C
(``C D
x``D E
=>``F H
x``I J
.``J K
UnitsInStock``K W
)``W X
)``X Y
.aa 
	ForMemberaa 
(aa 
destaa 
=>aa 
destaa #
.aa# $
Discontinuedaa$ 0
,aa0 1
srcaa2 5
=>aa6 8
srcaa9 <
.aa< =
MapFromaa= D
(aaD E
xaaE F
=>aaG I
xaaJ K
.aaK L
DiscontinuedaaL X
)aaX Y
)aaY Z
.bb 
	ForMemberbb 
(bb 
destbb 
=>bb 
destbb #
.bb# $
Descriptionbb$ /
,bb/ 0
srcbb1 4
=>bb5 7
srcbb8 ;
.bb; <
MapFrombb< C
(bbC D
xbbD E
=>bbF H
xbbI J
.bbJ K
QuantityPerUnitbbK Z
)bbZ [
)bb[ \
.cc 
	ForMembercc 
<cc 
PublisherModelDtocc (
>cc( )
(cc) *
destcc* .
=>cc/ 1
destcc2 6
.cc6 7
	Publishercc7 @
,cc@ A
srcccB E
=>ccF H
srcccI L
.ccL M
MapFromccM T
(ccT U
xccU V
=>ccW Y
xccZ [
.cc[ \
Suppliercc\ d
)ccd e
)cce f
.dd 
	ForMemberdd 
<dd 
Listdd 
<dd 
GenreModelDtodd )
>dd) *
>dd* +
(dd+ ,
destdd, 0
=>dd1 3
destdd4 8
.dd8 9
Genresdd9 ?
,dd? @
srcddA D
=>ddE G
srcddH K
.ddK L
MapFromddL S
(ddS T
xddT U
=>ddV X
newddY \
Listdd] a
<dda b
GenreModelDtoddb o
>ddo p
(ddp q
)ddq r
{dds t
newddu x
(ddx y
)ddy z
{dd{ |
Iddd} 
=
dd� �
x
dd� �
.
dd� �
ProductGenres
dd� �
[
dd� �
$num
dd� �
]
dd� �
.
dd� �

CategoryId
dd� �
}
dd� �
}
dd� �
)
dd� �
)
dd� �
;
dd� �
	CreateMapff 
<ff 
MongoCategoryff 
,ff  
GenreModelDtoff! .
>ff. /
(ff/ 0
)ff0 1
.gg 
	ForMembergg 
(gg 
destgg 
=>gg 
destgg #
.gg# $
Namegg$ (
,gg( )
srcgg* -
=>gg. 0
srcgg1 4
.gg4 5
MapFromgg5 <
(gg< =
xgg= >
=>gg? A
xggB C
.ggC D
CategoryNameggD P
)ggP Q
)ggQ R
.hh 
	ForMemberhh 
(hh 
desthh 
=>hh 
desthh #
.hh# $
Idhh$ &
,hh& '
srchh( +
=>hh, .
srchh/ 2
.hh2 3
MapFromhh3 :
(hh: ;
xhh; <
=>hh= ?
GuidHelpershh@ K
.hhK L
	IntToGuidhhL U
(hhU V
xhhV W
.hhW X

CategoryIdhhX b
)hhb c
)hhc d
)hhd e
;hhe f
	CreateMapjj 
<jj 
MongoCategoryjj 
,jj  
Genrejj! &
>jj& '
(jj' (
)jj( )
.kk 
	ForMemberkk 
(kk 
destkk 
=>kk 
destkk #
.kk# $
Namekk$ (
,kk( )
srckk* -
=>kk. 0
srckk1 4
.kk4 5
MapFromkk5 <
(kk< =
xkk= >
=>kk? A
xkkB C
.kkC D
CategoryNamekkD P
)kkP Q
)kkQ R
.ll 
	ForMemberll 
(ll 
destll 
=>ll 
destll #
.ll# $
Idll$ &
,ll& '
srcll( +
=>ll, .
srcll/ 2
.ll2 3
MapFromll3 :
(ll: ;
xll; <
=>ll= ?
GuidHelpersll@ K
.llK L
	IntToGuidllL U
(llU V
xllV W
.llW X

CategoryIdllX b
)llb c
)llc d
)lld e
;lle f
	CreateMapnn 
<nn 
MongoSuppliernn 
,nn  
PublisherModelDtonn! 2
>nn2 3
(nn3 4
)nn4 5
.oo 
	ForMemberoo 
(oo 
destoo 
=>oo 
destoo #
.oo# $
CompanyNameoo$ /
,oo/ 0
srcoo1 4
=>oo5 7
srcoo8 ;
.oo; <
MapFromoo< C
(ooC D
xooD E
=>ooF H
xooI J
.ooJ K
CompanyNameooK V
)ooV W
)ooW X
.pp 
	ForMemberpp 
(pp 
destpp 
=>pp 
destpp #
.pp# $
Idpp$ &
,pp& '
srcpp( +
=>pp, .
srcpp/ 2
.pp2 3
MapFrompp3 :
(pp: ;
xpp; <
=>pp= ?
GuidHelperspp@ K
.ppK L
	IntToGuidppL U
(ppU V
xppV W
.ppW X

SupplierIDppX b
)ppb c
)ppc d
)ppd e
;ppe f
	CreateMaprr 
<rr 
MongoSupplierrr 
,rr  
	Publisherrr! *
>rr* +
(rr+ ,
)rr, -
.ss 
	ForMemberss 
(ss 
destss 
=>ss 
destss #
.ss# $
CompanyNamess$ /
,ss/ 0
srcss1 4
=>ss5 7
srcss8 ;
.ss; <
MapFromss< C
(ssC D
xssD E
=>ssF H
xssI J
.ssJ K
CompanyNamessK V
)ssV W
)ssW X
.tt 
	ForMembertt 
(tt 
desttt 
=>tt 
desttt #
.tt# $
Idtt$ &
,tt& '
srctt( +
=>tt, .
srctt/ 2
.tt2 3
MapFromtt3 :
(tt: ;
xtt; <
=>tt= ?
GuidHelperstt@ K
.ttK L
	IntToGuidttL U
(ttU V
xttV W
.ttW X

SupplierIDttX b
)ttb c
)ttc d
)ttd e
;tte f
	CreateMapvv 
<vv 
MongoPublishervv  
,vv  !
	Publishervv" +
>vv+ ,
(vv, -
)vv- .
.vv. /

ReverseMapvv/ 9
(vv9 :
)vv: ;
;vv; <
	CreateMapww 
<ww 
MongoPublisherww  
,ww  !
PublisherModelDtoww" 3
>ww3 4
(ww4 5
)ww5 6
.xx 
	ForMemberxx 
(xx 
destxx 
=>xx 
destxx #
.xx# $
Idxx$ &
,xx& '
srcxx( +
=>xx, .
srcxx/ 2
.xx2 3
MapFromxx3 :
(xx: ;
xxx; <
=>xx= ?
xxx@ A
.xxA B
IdxxB D
)xxD E
)xxE F
.yy 
	ForMemberyy 
(yy 
destyy 
=>yy 
destyy #
.yy# $
CompanyNameyy$ /
,yy/ 0
srcyy1 4
=>yy5 7
srcyy8 ;
.yy; <
MapFromyy< C
(yyC D
xyyD E
=>yyF H
xyyI J
.yyJ K
CompanyNameyyK V
)yyV W
)yyW X
;yyX Y
	CreateMapzz 
<zz  
MongoProductCategoryzz &
,zz& '

GameGenreszz( 2
>zz2 3
(zz3 4
)zz4 5
.zz5 6

ReverseMapzz6 @
(zz@ A
)zzA B
;zzB C
	CreateMap{{ 
<{{  
MongoProductCategory{{ &
,{{& '
GenreModelDto{{( 5
>{{5 6
({{6 7
){{7 8
.{{8 9

ReverseMap{{9 C
({{C D
){{D E
;{{E F
	CreateMap|| 
<||  
MongoProductPlatform|| &
,||& '
GamePlatform||( 4
>||4 5
(||5 6
)||6 7
.||7 8

ReverseMap||8 B
(||B C
)||C D
;||D E
	CreateMap}} 
<}}  
MongoProductPlatform}} &
,}}& '
PlatformModelDto}}( 8
>}}8 9
(}}9 :
)}}: ;
.}}; <

ReverseMap}}< F
(}}F G
)}}G H
;}}H I
	CreateMap 
< 
MongoShipper 
, 
ShipperModelDto  /
>/ 0
(0 1
)1 2
.2 3

ReverseMap3 =
(= >
)> ?
;? @
	CreateMap
�� 
<
�� 

MongoOrder
�� 
,
�� 
Order
�� #
>
��# $
(
��$ %
)
��% &
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Id
��$ &
,
��& '
src
��( +
=>
��, .
src
��/ 2
.
��2 3
MapFrom
��3 :
(
��: ;
x
��; <
=>
��= ?
GuidHelpers
��@ K
.
��K L
	IntToGuid
��L U
(
��U V
x
��V W
.
��W X
OrderId
��X _
)
��_ `
)
��` a
)
��a b
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $

CustomerId
��$ .
,
��. /
src
��0 3
=>
��4 6
src
��7 :
.
��: ;
MapFrom
��; B
(
��B C
x
��C D
=>
��E G
Guid
��H L
.
��L M
Empty
��M R
)
��R S
)
��S T
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
	OrderDate
��$ -
,
��- .
src
��/ 2
=>
��3 5
src
��6 9
.
��9 :
MapFrom
��: A
(
��A B
x
��B C
=>
��D F
x
��G H
.
��H I
	OrderDate
��I R
)
��R S
)
��S T
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $

OrderGames
��$ .
,
��. /
src
��0 3
=>
��4 6
src
��7 :
.
��: ;
Ignore
��; A
(
��A B
)
��B C
)
��C D
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $

EmployeeId
��$ .
,
��. /
src
��0 3
=>
��4 6
src
��7 :
.
��: ;
Ignore
��; A
(
��A B
)
��B C
)
��C D
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
ShipVia
��$ +
,
��+ ,
src
��- 0
=>
��1 3
src
��4 7
.
��7 8
Ignore
��8 >
(
��> ?
)
��? @
)
��@ A
.
�� 

ReverseMap
�� 
(
�� 
)
�� 
;
�� 
	CreateMap
�� 
<
�� 

MongoOrder
�� 
,
�� 
OrderModelDto
�� +
>
��+ ,
(
��, -
)
��- .
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Id
��$ &
,
��& '
src
��( +
=>
��, .
src
��/ 2
.
��2 3
MapFrom
��3 :
(
��: ;
x
��; <
=>
��= ?
GuidHelpers
��@ K
.
��K L
	IntToGuid
��L U
(
��U V
x
��V W
.
��W X
OrderId
��X _
)
��_ `
)
��` a
)
��a b
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $

CustomerId
��$ .
,
��. /
src
��0 3
=>
��4 6
src
��7 :
.
��: ;
MapFrom
��; B
(
��B C
x
��C D
=>
��E G
x
��H I
.
��I J

CustomerId
��J T
)
��T U
)
��U V
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Date
��$ (
,
��( )
src
��* -
=>
��. 0
src
��1 4
.
��4 5
MapFrom
��5 <
(
��< =
x
��= >
=>
��? A
x
��B C
.
��C D
	OrderDate
��D M
)
��M N
)
��N O
.
�� 

ReverseMap
�� 
(
�� 
)
�� 
;
�� 
	CreateMap
�� 
<
�� 
MongoOrderModel
�� !
,
��! "
OrderModelDto
��# 0
>
��0 1
(
��1 2
)
��2 3
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Id
��$ &
,
��& '
src
��( +
=>
��, .
src
��/ 2
.
��2 3
MapFrom
��3 :
(
��: ;
x
��; <
=>
��= ?
x
��@ A
.
��A B
Id
��B D
)
��D E
)
��E F
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $

CustomerId
��$ .
,
��. /
src
��0 3
=>
��4 6
src
��7 :
.
��: ;
MapFrom
��; B
(
��B C
x
��C D
=>
��E G
x
��H I
.
��I J

CustomerId
��J T
)
��T U
)
��U V
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Date
��$ (
,
��( )
src
��* -
=>
��. 0
src
��1 4
.
��4 5
MapFrom
��5 <
(
��< =
x
��= >
=>
��? A
x
��B C
.
��C D
Date
��D H
.
��H I
ToString
��I Q
(
��Q R
$str
��R ^
)
��^ _
)
��_ `
)
��` a
;
��a b
	CreateMap
�� 
<
�� 

MongoOrder
�� 
,
�� 
MongoOrderModel
�� -
>
��- .
(
��. /
)
��/ 0
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Id
��$ &
,
��& '
src
��( +
=>
��, .
src
��/ 2
.
��2 3
MapFrom
��3 :
(
��: ;
x
��; <
=>
��= ?
GuidHelpers
��@ K
.
��K L
	IntToGuid
��L U
(
��U V
x
��V W
.
��W X
OrderId
��X _
)
��_ `
)
��` a
)
��a b
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $

CustomerId
��$ .
,
��. /
src
��0 3
=>
��4 6
src
��7 :
.
��: ;
MapFrom
��; B
(
��B C
x
��C D
=>
��E G
x
��H I
.
��I J

CustomerId
��J T
)
��T U
)
��U V
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Date
��$ (
,
��( )
src
��* -
=>
��. 0
src
��1 4
.
��4 5
MapFrom
��5 <
(
��< =
x
��= >
=>
��? A
DateTime
��B J
.
��J K

ParseExact
��K U
(
��U V
x
��V W
.
��W X
	OrderDate
��X a
,
��a b
$str
��c |
,
��| }
CultureInfo��~ �
.��� � 
InvariantCulture��� �
)��� �
)��� �
)��� �
.
�� 

ReverseMap
�� 
(
�� 
)
�� 
;
�� 
	CreateMap
�� 
<
�� 
AppUser
�� 
,
�� 
CustomerDto
�� &
>
��& '
(
��' (
)
��( )
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Id
��$ &
,
��& '
src
��( +
=>
��, .
src
��/ 2
.
��2 3
MapFrom
��3 :
(
��: ;
x
��; <
=>
��= ?
x
��@ A
.
��A B
Id
��B D
)
��D E
)
��E F
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Name
��$ (
,
��( )
src
��* -
=>
��. 0
src
��1 4
.
��4 5
MapFrom
��5 <
(
��< =
x
��= >
=>
��? A
x
��B C
.
��C D
UserName
��D L
)
��L M
)
��M N
;
��N O
	CreateMap
�� 
<
�� 
AppRole
�� 
,
�� 
	RoleModel
�� $
>
��$ %
(
��% &
)
��& '
.
�� 
	ForMember
�� 
(
�� 
dest
�� 
=>
�� 
dest
�� #
.
��# $
Name
��$ (
,
��( )
src
��* -
=>
��. 0
src
��1 4
.
��4 5
MapFrom
��5 <
(
��< =
x
��= >
=>
��? A
x
��B C
.
��C D
Name
��D H
)
��H I
)
��I J
;
��J K
}
�� 
}�� �
hD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Helpers\ValidationHelpers.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Helpers 
;  
internal 
static	 
class 
ValidationHelpers '
{ 
internal 
static 
void !
CyclicReferenceHelper .
(. /
List/ 3
<3 4
Genre4 9
>9 :
genres; A
,A B
ListC G
<G H
GenreH M
>M N
forbiddenListO \
,\ ]
Guid^ b
parentIdc k
)k l
{		 
var

 
childGenres

 
=

 
genres

  
.

  !
Where

! &
(

& '
x

' (
=>

) +
x

, -
.

- .
ParentGenreId

. ;
==

< >
parentId

? G
)

G H
;

H I
if 

( 
childGenres 
. 
Any 
( 
) 
) 
{ 	
forbiddenList 
. 
AddRange "
(" #
childGenres# .
). /
;/ 0
foreach 
( 
var 
genre 
in !
childGenres" -
)- .
{ !
CyclicReferenceHelper %
(% &
genres& ,
,, -
forbiddenList. ;
,; <
genre= B
.B C
IdC E
)E F
;F G
} 
} 	
} 
} �8
jD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Helpers\ValidatorExtensions.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Helpers 
;  
internal 
static	 
class 
ValidatorExtensions )
{ 
internal		 
static		 
async		 
Task		 
ValidatePublisher		 0
(		0 1
this		1 5(
PublisherDtoWrapperValidator		6 R
	validator		S \
,		\ ]
PublisherDtoWrapper		^ q
publisherModel			r �
)
		� �
{

 
var 
result 
= 
await 
	validator $
.$ %
ValidateAsync% 2
(2 3
publisherModel3 A
.A B
	PublisherB K
)K L
;L M
if 

( 
! 
result 
. 
IsValid 
) 
{ 	
throw 
new 
ArgumentException '
(' (
result( .
.. /
Errors/ 5
[5 6
$num6 7
]7 8
.8 9
ToString9 A
(A B
)B C
)C D
;D E
} 	
} 
internal 
static 
async 
Task 
ValidatePlatform /
(/ 0
this0 4'
PlatformDtoWrapperValidator5 P
	validatorQ Z
,Z [
PlatformDtoWrapper\ n
platformModelo |
)| }
{ 
var 
result 
= 
await 
	validator $
.$ %
ValidateAsync% 2
(2 3
platformModel3 @
.@ A
PlatformA I
)I J
;J K
if 

( 
! 
result 
. 
IsValid 
) 
{ 	
throw 
new 
ArgumentException '
(' (
result( .
.. /
Errors/ 5
[5 6
$num6 7
]7 8
.8 9
ToString9 A
(A B
)B C
)C D
;D E
} 	
} 
internal 
static 
async 
Task "
ValidateGenreForAdding 5
(5 6
this6 :'
GenreDtoWrapperAddValidator; V
	validatorW `
,` a
GenreDtoWrapperb q

genreModelr |
)| }
{ 
var 
result 
= 
await 
	validator $
.$ %
ValidateAsync% 2
(2 3

genreModel3 =
.= >
Genre> C
)C D
;D E
if 

( 
! 
result 
. 
IsValid 
) 
{ 	
throw   
new   
ArgumentException   '
(  ' (
result  ( .
.  . /
Errors  / 5
[  5 6
$num  6 7
]  7 8
.  8 9
ToString  9 A
(  A B
)  B C
)  C D
;  D E
}!! 	
}"" 
internal$$ 
static$$ 
async$$ 
Task$$ $
ValidateGenreForUpdating$$ 7
($$7 8
this$$8 <*
GenreDtoWrapperUpdateValidator$$= [
	validator$$\ e
,$$e f
GenreDtoWrapper$$g v

genreModel	$$w �
)
$$� �
{%% 
var&& 
result&& 
=&& 
await&& 
	validator&& $
.&&$ %
ValidateAsync&&% 2
(&&2 3

genreModel&&3 =
.&&= >
Genre&&> C
)&&C D
;&&D E
if'' 

('' 
!'' 
result'' 
.'' 
IsValid'' 
)'' 
{(( 	
throw)) 
new)) 
ArgumentException)) '
())' (
result))( .
.)). /
Errors))/ 5
[))5 6
$num))6 7
]))7 8
.))8 9
ToString))9 A
())A B
)))B C
)))C D
;))D E
}** 	
}++ 
internal-- 
static-- 
async-- 
Task-- 
ValidateGame-- +
(--+ ,
this--, 0#
GameDtoWrapperValidator--1 H
	validator--I R
,--R S
GameDtoWrapper--T b
	gameModel--c l
)--l m
{.. 
var// 
result// 
=// 
await// 
	validator// $
.//$ %
ValidateAsync//% 2
(//2 3
	gameModel//3 <
)//< =
;//= >
if00 

(00 
!00 
result00 
.00 
IsValid00 
)00 
{11 	
throw22 
new22 
ArgumentException22 '
(22' (
result22( .
.22. /
Errors22/ 5
[225 6
$num226 7
]227 8
.228 9
ToString229 A
(22A B
)22B C
)22C D
;22D E
}33 	
}44 
internal66 
static66 
async66 
Task66 
ValidateVisaPayment66 2
(662 3
this663 7 
VisaPaymentValidator668 L
	validator66M V
,66V W
PaymentModelDto66X g
payment66h o
)66o p
{77 
var88 
result88 
=88 
await88 
	validator88 $
.88$ %
ValidateAsync88% 2
(882 3
payment883 :
)88: ;
;88; <
if99 

(99 
!99 
result99 
.99 
IsValid99 
)99 
{:: 	
throw;; 
new;; 
ArgumentException;; '
(;;' (
result;;( .
.;;. /
Errors;;/ 5
[;;5 6
$num;;6 7
];;7 8
.;;8 9
ToString;;9 A
(;;A B
);;B C
);;C D
;;;D E
}<< 	
}== 
internal?? 
static?? 
async?? 
Task?? 
ValidateComment?? .
(??. /
this??/ 3$
CommentModelDtoValidator??4 L
	validator??M V
,??V W
CommentModelDto??X g
comment??h o
)??o p
{@@ 
varAA 
resultAA 
=AA 
awaitAA 
	validatorAA $
.AA$ %
ValidateAsyncAA% 2
(AA2 3
commentAA3 :
)AA: ;
;AA; <
ifBB 

(BB 
!BB 
resultBB 
.BB 
IsValidBB 
)BB 
{CC 	
throwDD 
newDD 
ArgumentExceptionDD '
(DD' (
resultDD( .
.DD. /
ErrorsDD/ 5
[DD5 6
$numDD6 7
]DD7 8
.DD8 9
ToStringDD9 A
(DDA B
)DDB C
)DDC D
;DDD E
}EE 	
}FF 
}GG �
|D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Extensions\ClaimsPrincipalExtensions.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !

Extensions! +
;+ ,
public 
static 
class %
ClaimsPrincipalExtensions -
{ 
public 

static 
string 
GetJwtSubject &
(& '
this' +
ClaimsPrincipal, ;
	principal< E
)E F
{		 !
ArgumentNullException

 
.

 
ThrowIfNull

 )
(

) *
	principal

* 3
)

3 4
;

4 5
var 
claim 
= 
	principal 
. 
	FindFirst '
(' (

ClaimTypes( 2
.2 3
NameIdentifier3 A
)A B
;B C
return 
claim 
? 
. 
Value 
; 
} 
public 

static 
string 
GetJwtSubjectId (
(( )
this) -
ClaimsPrincipal. =
	principal> G
)G H
{ !
ArgumentNullException 
. 
ThrowIfNull )
() *
	principal* 3
)3 4
;4 5
var 
id 
= 
	principal 
. 
Claims !
.! "
Where" '
(' (
c( )
=>* ,
c- .
.. /
Type/ 3
==4 6

JwtHelpers7 A
.A B
UserIdClaimB M
)M N
.N O
SelectO U
(U V
xV W
=>X Z
x[ \
.\ ]
Value] b
)b c
.c d
FirstOrDefaultd r
(r s
)s t
;t u
return 
id 
; 
} 
} �
sD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Helpers\RoleHierarchyHelper.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Helpers! (
;( )
internal 
class	 
RoleHierarchyHelper "
{ 
private 
readonly 
RoleManager  
<  !
AppRole! (
>( )
_roleManager* 6
;6 7
internal

 
RoleHierarchyHelper

  
(

  !
RoleManager

! ,
<

, -
AppRole

- 4
>

4 5
roleManager

6 A
)

A B
{ 
_roleManager 
= 
roleManager "
;" #
} 
internal 
async 
Task 
< 
List 
< 
string #
># $
>$ %"
GetEffectiveRolesAsync& <
(< =
AppUser= D
userE I
,I J
UserManagerK V
<V W
AppUserW ^
>^ _
userManager` k
)k l
{ 
var 
roles 
= 
await 
userManager %
.% &
GetRolesAsync& 3
(3 4
user4 8
)8 9
;9 :
var 
allRoles 
= 
new 
HashSet "
<" #
string# )
>) *
(* +
roles+ 0
)0 1
;1 2
foreach 
( 
var 
roleName 
in  
roles! &
)& '
{ 	
var 
role 
= 
await 
_roleManager )
.) *
FindByNameAsync* 9
(9 :
roleName: B
)B C
;C D
if 
( 
role 
!= 
null 
) 
{ 
await 
AddParentRolesAsync )
() *
role* .
,. /
allRoles0 8
)8 9
;9 :
} 
} 	
return 
[ 
.. 
allRoles 
] 
; 
} 
private   
async   
Task   
AddParentRolesAsync   *
(  * +
AppRole  + 2
role  3 7
,  7 8
HashSet  9 @
<  @ A
string  A G
>  G H
allRoles  I Q
)  Q R
{!! 
while"" 
("" 
role"" 
."" 
ParentRoleId""  
!=""! #
null""$ (
)""( )
{## 	
var$$ 

parentRole$$ 
=$$ 
await$$ "
_roleManager$$# /
.$$/ 0
FindByIdAsync$$0 =
($$= >
role$$> B
.$$B C
ParentRoleId$$C O
)$$O P
;$$P Q
if%% 
(%% 

parentRole%% 
==%% 
null%% "
)%%" #
{&& 
break'' 
;'' 
}(( 
if** 
(** 
!** 
allRoles** 
.** 
Contains** "
(**" #

parentRole**# -
.**- .
Name**. 2
!**2 3
)**3 4
)**4 5
{++ 
allRoles,, 
.,, 
Add,, 
(,, 

parentRole,, '
.,,' (
Name,,( ,
!,,, -
),,- .
;,,. /
}-- 
role// 
=// 

parentRole// 
;// 
}00 	
}11 
}22 �)
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\JWT\JWTHelpers.cs
	namespace

 	
	Gamestore


 
.

 
BLL

 
.

 
Identity

  
.

  !
JWT

! $
;

$ %
public 
static 
class 

JwtHelpers 
{ 
public 

static 
string 
UserIdClaim $
{% &
get' *
;* +
}, -
=. /
$str0 8
;8 9
public 

static 
async 
Task 
< 
string #
># $
GenerateJwtToken% 5
(5 6
UserManager6 A
<A B
AppUserB I
>I J
userManagerK V
,V W
RoleManagerX c
<c d
AppRoled k
>k l
roleManagerm x
,x y
IConfiguration	z �
configuration
� �
,
� �
AppUser
� �
?
� �
user
� �
)
� �
{ 
var 
claims 
= 
new 
List 
< 
Claim #
># $
{ 	
new 
( #
JwtRegisteredClaimNames '
.' (
Sub( +
,+ ,
user- 1
.1 2
UserName2 :
!: ;
); <
,< =
new 
( #
JwtRegisteredClaimNames '
.' (
Jti( +
,+ ,
Guid- 1
.1 2
NewGuid2 9
(9 :
): ;
.; <
ToString< D
(D E
)E F
)F G
,G H
new 
( 

ClaimTypes 
. 
NameIdentifier )
,) *
user+ /
./ 0
UserName0 8
!8 9
)9 :
,: ;
new 
( 
UserIdClaim 
, 
user !
.! "
Id" $
)$ %
,% &
} 	
;	 

var 
roleHierarchyHelper 
=  !
new" %
RoleHierarchyHelper& 9
(9 :
roleManager: E
)E F
;F G
var 
effectiveRoles 
= 
await "
roleHierarchyHelper# 6
.6 7"
GetEffectiveRolesAsync7 M
(M N
userN R
,R S
userManagerT _
)_ `
;` a
foreach 
( 
var 
role 
in 
effectiveRoles +
)+ ,
{ 	
claims 
. 
Add 
( 
new 
Claim  
(  !

ClaimTypes! +
.+ ,
Role, 0
,0 1
role2 6
)6 7
)7 8
;8 9
var   
appRole   
=   
await   
roleManager    +
.  + ,
FindByNameAsync  , ;
(  ; <
role  < @
)  @ A
;  A B
var!! 

roleClaims!! 
=!! 
await!! "
roleManager!!# .
.!!. /
GetClaimsAsync!!/ =
(!!= >
appRole!!> E
!!!E F
)!!F G
;!!G H
claims"" 
."" 
AddRange"" 
("" 

roleClaims"" &
)""& '
;""' (
}## 	
var%% 
key%% 
=%% 
new%%  
SymmetricSecurityKey%% *
(%%* +
Encoding%%+ 3
.%%3 4
UTF8%%4 8
.%%8 9
GetBytes%%9 A
(%%A B
configuration%%B O
[%%O P
$str%%P Y
]%%Y Z
!%%Z [
)%%[ \
)%%\ ]
;%%] ^
var&& 
creds&& 
=&& 
new&& 
SigningCredentials&& *
(&&* +
key&&+ .
,&&. /
SecurityAlgorithms&&0 B
.&&B C

HmacSha256&&C M
)&&M N
;&&N O
var(( 
token(( 
=(( 
new(( 
JwtSecurityToken(( (
(((( )
issuer)) 
:)) 
configuration)) 
[)) 
$str)) *
]))* +
,))+ ,
audience** 
:** 
configuration** 
[**  
$str**  .
]**. /
,**/ 0
claims++ 
:++ 
claims++ 
,++ 
expires,, 
:,, 
DateTime,, 
.,, 
UtcNow,,  
.,,  !

AddMinutes,,! +
(,,+ ,
$num,,, .
),,. /
,,,/ 0
signingCredentials-- 
:-- 
creds-- !
)--! "
;--" #
var// 
generatedToken// 
=// 
new//  #
JwtSecurityTokenHandler//! 8
(//8 9
)//9 :
.//: ;

WriteToken//; E
(//E F
token//F K
)//K L
;//L M
return00 
generatedToken00 
;00 
}11 
}22 �
jD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\Permissions.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
static 
class 
Permissions 
{ 
private 
static 
readonly 

Dictionary &
<& '
string' -
,- .
string/ 5
>5 6$
PermissionListDictionary7 O
=P Q
newR U
(U V
)V W
{ 
{ 	
$str
 #
,# $
$str% 1
}2 3
,3 4
{ 	
$str
 &
,& '
$str( 7
}8 9
,9 :
{		 	
$str		
  
,		  !
$str		" +
}		, -
,		- .
{

 	
$str


 $
,

$ %
$str

& 3
}

4 5
,

5 6
{ 	
$str
 $
,$ %
$str& 3
}4 5
,5 6
{ 	
$str
 %
,% &
$str' 5
}6 7
,7 8
{ 	
$str
 '
,' (
$str) 9
}: ;
,; <
{ 	
$str
 #
,# $
$str% 1
}2 3
,3 4
{ 	
$str
 %
,% &
$str' 5
}6 7
,7 8
{ 	
$str
 $
,$ %
$str& 3
}4 5
,5 6
{ 	
$str
 !
,! "
$str# -
}. /
,/ 0
{ 	
$str
 )
,) *
$str+ =
}> ?
,? @
} 
; 
public 

static 

Dictionary 
< 
string #
,# $
string% +
>+ ,
PermissionList- ;
=>< >$
PermissionListDictionary? W
;W X
} �
hD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\RoleModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
record 
	RoleModel 
{ 
public 

Guid 
? 
Id 
{ 
get 
; 
set 
; 
}  !
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
public		 

List		 
<		 
string		 
>		 
?		 
Permissions		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
}

 �
kD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\RoleModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
record 
RoleModelDto 
{ 
public 

	RoleModel 
Role 
{ 
get 
;  
set! $
;$ %
}& '
public 

List 
< 
string 
> 
? 
Permissions $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} �
lD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\TokenModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
record 
TokenModelDto 
{ 
public 

string 
Token 
{ 
get 
; 
set "
;" #
}$ %
} �
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\UserDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
record 
UserDto 
{ 
public 

	UserModel 
User 
{ 
get 
;  
set! $
;$ %
}& '
public 

List 
< 
string 
> 
Roles 
{ 
get  #
;# $
set% (
;( )
}* +
public		 

string		 
Password		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
}

 �
hD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\UserModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
record 
	UserModel 
{ 
public 

string 
Id 
{ 
get 
; 
set 
;  
}! "
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
} �
jD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Identity\Models\UserRoleDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Identity  
.  !
Models! '
;' (
public 
record 
UserRoleDto 
{ 
public 

string 
Id 
{ 
get 
; 
set 
;  
}! "
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
} �
iD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\ICommentService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Interfaces "
;" #
public 
	interface 
ICommentService  
{ 
Task		 *
BanCustomerFromCommentingAsync			 '
(		' (
BanDto		( .

banDetails		/ 9
,		9 :
UserManager		; F
<		F G
AppUser		G N
>		N O
userManager		P [
)		[ \
;		\ ]
List 
< 	
string	 
> 
GetBanDurations  
(  !
)! "
;" #
} �"
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IGameService.cs
	namespace 	
	Gamestore
 
. 
Services 
. 

Interfaces '
;' (
public		 
	interface		 
IGameService		 
{

 
Task 
AddGameAsync	 
( 
GameDtoWrapper $
	gameModel% .
). /
;/ 0
Task 
AddGameToCartAsync	 
( 
Guid  

customerId! +
,+ ,
string- 3
gameKey4 ;
,; <
int= @
quantityA I
)I J
;J K
Task 
DeleteGameByIdAsync	 
( 
Guid !
gameId" (
)( )
;) *
Task  
DeleteGameByKeyAsync	 
( 
string $
gameKey% ,
), -
;- .
Task 
< 	
List	 
< 
GameModelDto 
> 
> 
GetAllGamesAsync -
(- .
bool. 2
canSeeDeletedGames3 E
)E F
;F G
Task 
< 	
FilteredGamesDto	 
> !
GetFilteredGamesAsync 0
(0 1
GameFiltersDto1 ?
gameFilters@ K
,K L
boolM Q
canSeeDeletedGamesR d
)d e
;e f
Task 
< 	
GameModelDto	 
> 
GetGameByIdAsync '
(' (
Guid( ,
gameId- 3
)3 4
;4 5
Task 
< 	
GameModelDto	 
> 
GetGameByKeyAsync (
(( )
string) /
key0 3
)3 4
;4 5
Task 
< 	
IEnumerable	 
< 
GenreModelDto "
>" #
># $#
GetGenresByGameKeyAsync% <
(< =
string= C
gameKeyD K
)K L
;L M
Task 
< 	
IEnumerable	 
< 
PlatformModelDto %
>% &
>& '&
GetPlatformsByGameKeyAsync( B
(B C
stringC I
gameKeyJ Q
)Q R
;R S
Task 
< 	
PublisherModelDto	 
> &
GetPublisherByGameKeyAsync 6
(6 7
string7 =
gameKey> E
)E F
;F G
Task!! #
SoftDeleteGameByIdAsync!!	  
(!!  !
Guid!!! %
gameId!!& ,
)!!, -
;!!- .
Task## $
SoftDeleteGameByKeyAsync##	 !
(##! "
string##" (
gameKey##) 0
)##0 1
;##1 2
Task%% 
UpdateGameAsync%%	 
(%% 
GameDtoWrapper%% '
	gameModel%%( 1
)%%1 2
;%%2 3
Task'' 
<'' 	
string''	 
>'' !
AddCommentToGameAsync'' &
(''& '
string''' -
userName''. 6
,''6 7
string''8 >
gameKey''? F
,''F G
CommentModelDto''H W
comment''X _
,''_ `
UserManager''a l
<''l m
AppUser''m t
>''t u
userManager	''v �
)
''� �
;
''� �
Task)) 
<)) 	
IEnumerable))	 
<)) 
CommentModel)) !
>))! "
>))" #%
GetCommentsByGameKeyAsync))$ =
())= >
string))> D
gameKey))E L
)))L M
;))M N
Task++ 
DeleteCommentAsync++	 
(++ 
string++ "
userName++# +
,+++ ,
string++- 3
gameKey++4 ;
,++; <
Guid++= A
	commentId++B K
,++K L
bool++M Q
canModerate++R ]
)++] ^
;++^ _
List-- 
<-- 	
string--	 
>--  
GetPaginationOptions-- %
(--% &
)--& '
;--' (
List// 
<// 	
string//	 
>// !
GetPublishDateOptions// &
(//& '
)//' (
;//( )
List11 
<11 	
string11	 
>11 
GetSortingOptions11 "
(11" #
)11# $
;11$ %
}22 �
gD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IGenreService.cs
	namespace 	
	Gamestore
 
. 
Services 
. 

Interfaces '
;' (
public 
	interface 
IGenreService 
{ 
Task 
AddGenreAsync	 
( 
GenreDtoWrapper &

genreModel' 1
)1 2
;2 3
Task

 
<

 	
IEnumerable

	 
<

 
GameModelDto

 !
>

! "
>

" # 
GetGamesByGenreAsync

$ 8
(

8 9
Guid

9 =
genreId

> E
)

E F
;

F G
Task 
< 	
GenreModelDto	 
> 
GetGenreByIdAsync )
() *
Guid* .
genreId/ 6
)6 7
;7 8
Task 
< 	
IEnumerable	 
< 
GenreModelDto "
>" #
># $
GetAllGenresAsync% 6
(6 7
)7 8
;8 9
Task 
UpdateGenreAsync	 
( 
GenreDtoWrapper )

genreModel* 4
)4 5
;5 6
Task 
DeleteGenreAsync	 
( 
Guid 
genreId &
)& '
;' (
Task 
< 	
IEnumerable	 
< 
GenreModelDto "
>" #
># $'
GetGenresByParentGenreAsync% @
(@ A
GuidA E
genreIdF M
)M N
;N O
} �
gD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IOrderService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Interfaces "
;" #
public 
	interface 
IOrderService 
{ 
Task  
DeleteOrderByIdAsync	 
( 
Guid "
orderId# *
)* +
;+ ,
Task

 
<

 	
List

	 
<

 
OrderModelDto

 
>

 
>

 
GetAllOrdersAsync

 /
(

/ 0
)

0 1
;

1 2
Task 
< 	
List	 
< 
OrderDetailsDto 
> 
> $
GetCartByCustomerIdAsync  8
(8 9
Guid9 =

customerId> H
)H I
;I J
Task 
< 	
OrderModelDto	 
> 
GetOrderByIdAsync )
() *
Guid* .
orderId/ 6
)6 7
;7 8
Task 
< 	
List	 
< 
OrderDetailsDto 
> 
> )
GetOrderDetailsByOrderIdAsync  =
(= >
Guid> B
orderIdC J
)J K
;K L
Task 
< 	
byte	 
[ 
] 
> 
CreateInvoicePdf !
(! "
PaymentModelDto" 1
payment2 9
,9 :
CustomerDto; F
customerG O
)O P
;P Q
Task 
PayWithVisaAsync	 
( 
PaymentModelDto )
payment* 1
,1 2
CustomerDto3 >
customer? G
)G H
;H I
Task 
PayWithIboxAsync	 
( 
PaymentModelDto )
payment* 1
,1 2
CustomerDto3 >
customer? G
)G H
;H I
Task #
RemoveGameFromCartAsync	  
(  !
Guid! %

customerId& 0
,0 1
string2 8
gameKey9 @
,@ A
intB E
quantityF N
)N O
;O P
PaymentMethodsDto 
GetPaymentMethods '
(' (
)( )
;) *
Task 
< 	
List	 
< 
OrderModelDto 
> 
> !
GetOrdersHistoryAsync 3
(3 4
string4 :
?: ;
	startDate< E
,E F
stringG M
?M N
endDateO V
)V W
;W X
Task 
	ShipAsync	 
( 
string 
id 
) 
; 
Task   "
AddProductToOrderAsync  	 
(    
string    &
orderId  ' .
,  . /
string  0 6

productKey  7 A
)  A B
;  B C
}!! �
jD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IPlatformService.cs
	namespace 	
	Gamestore
 
. 
Services 
. 

Interfaces '
;' (
public 
	interface 
IPlatformService !
{ 
Task 
AddPlatformAsync	 
( 
PlatformDtoWrapper ,
platformModel- :
): ;
;; <
Task

 
<

 	
IEnumerable

	 
<

 
GameModelDto

 !
>

! "
>

" #%
GetGamesByPlatformIdAsync

$ =
(

= >
Guid

> B

platformId

C M
)

M N
;

N O
Task 
< 	
PlatformModelDto	 
>  
GetPlatformByIdAsync /
(/ 0
Guid0 4

platformId5 ?
)? @
;@ A
Task 
< 	
IEnumerable	 
< 
PlatformModelDto %
>% &
>& ' 
GetAllPlatformsAsync( <
(< =
)= >
;> ?
Task 
UpdatePlatformAsync	 
( 
PlatformDtoWrapper /
platformModel0 =
)= >
;> ?
Task #
DeletePlatformByIdAsync	  
(  !
Guid! %

platformId& 0
)0 1
;1 2
} �
kD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IPublisherService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Interfaces "
;" #
public 
	interface 
IPublisherService "
{ 
Task 
AddPublisherAsync	 
( 
PublisherDtoWrapper .
publisherModel/ =
)= >
;> ?
Task

 #
DeletPublisherByIdAsync

	  
(

  !
Guid

! %
publisherId

& 1
)

1 2
;

2 3
Task 
< 	
IEnumerable	 
< 
PublisherModelDto &
>& '
>' (!
GetAllPublishersAsync) >
(> ?
)? @
;@ A
Task 
< 	
IEnumerable	 
< 
GameModelDto !
>! "
>" #&
GetGamesByPublisherIdAsync$ >
(> ?
Guid? C
publisherIdD O
)O P
;P Q
Task 
< 	
IEnumerable	 
< 
GameModelDto !
>! "
>" #(
GetGamesByPublisherNameAsync$ @
(@ A
stringA G
publisherNameH U
)U V
;V W
Task  
UpdatePublisherAsync	 
( 
PublisherDtoWrapper 1
publisherModel2 @
)@ A
;A B
Task 
< 	
PublisherModelDto	 
> !
GetPublisherByIdAsync 1
(1 2
Guid2 6
publisherId7 B
)B C
;C D
Task 
< 	
PublisherModelDto	 
> *
GetPublisherByCompanyNameAsync :
(: ;
string; A
companyNameB M
)M N
;N O
} �
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IRoleService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Interfaces "
;" #
public 
	interface 
IRoleService 
{ 
List		 
<		 	
	RoleModel			 
>		 
GetAllRoles		 
(		  
RoleManager		  +
<		+ ,
AppRole		, 3
>		3 4
roleManager		5 @
)		@ A
;		A B
List 
< 	
string	 
> 
GetAllPermissions "
(" #
)# $
;$ %
Task 
< 	
IdentityResult	 
> 
AddRoleAsync %
(% &
RoleManager& 1
<1 2
AppRole2 9
>9 :
roleManager; F
,F G
RoleModelDtoH T
roleU Y
)Y Z
;Z [
Task 
< 	
IdentityResult	 
> 
DeleteRoleByIdAsync ,
(, -
RoleManager- 8
<8 9
AppRole9 @
>@ A
roleManagerB M
,M N
GuidO S
roleIdT Z
)Z [
;[ \
Task 
< 	
	RoleModel	 
> 
GetRoleByIdAsync $
($ %
RoleManager% 0
<0 1
AppRole1 8
>8 9
roleManager: E
,E F
GuidG K
roleIdL R
)R S
;S T
Task 
< 	
List	 
< 
string 
> 
> '
GetPermissionsByRoleIdAsync 2
(2 3
RoleManager3 >
<> ?
AppRole? F
>F G
roleManagerH S
,S T
GuidU Y
roleIdZ `
)` a
;a b
Task 
< 	
IdentityResult	 
> 
UpdateRoleAsync (
(( )
RoleManager) 4
<4 5
AppRole5 <
>< =
roleManager> I
,I J
RoleModelDtoK W
roleDtoX _
)_ `
;` a
} �
iD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IShipperService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Interfaces "
;" #
public 
	interface 
IShipperService  
{ 
Task 
< 	
List	 
< 
ShipperModelDto 
> 
> 
GetAllShippersAsync  3
(3 4
)4 5
;5 6
} �
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Interfaces\IUserService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Interfaces "
;" #
public		 
	interface		 
IUserService		 
{

 
Task 
AddUserAsync	 
( 
UserManager !
<! "
AppUser" )
>) *
userManager+ 6
,6 7
RoleManager8 C
<C D
AppRoleD K
>K L
roleManagerM X
,X Y
UserDtoZ a
userb f
)f g
;g h
Task 
DeleteUserAsync	 
( 
UserManager $
<$ %
AppUser% ,
>, -
userManager. 9
,9 :
string; A
userIdB H
)H I
;I J
List 
< 	
CustomerDto	 
> 
GetAllUsers !
(! "
UserManager" -
<- .
AppUser. 5
>5 6
userManager7 B
)B C
;C D
Task 
< 	
CustomerDto	 
> 
GetUserByIdAsync &
(& '
UserManager' 2
<2 3
AppUser3 :
>: ;
userManager< G
,G H
stringI O
userIdP V
)V W
;W X
Task 
< 	
List	 
< 
UserRoleDto 
> 
>  
GetUserRolesByUserId 0
(0 1
UserManager1 <
<< =
AppUser= D
>D E
userManagerF Q
,Q R
RoleManagerS ^
<^ _
AppRole_ f
>f g
roleManagerh s
,s t
stringu {
userId	| �
)
� �
;
� �
Task 
< 	
string	 
> 

LoginAsync 
( 
UserManager '
<' (
AppUser( /
>/ 0
userManager1 <
,< =
RoleManager> I
<I J
AppRoleJ Q
>Q R
roleManagerS ^
,^ _
IConfiguration` n
configurationo |
,| }
LoginModelDto	~ �
login
� �
)
� �
;
� �
Task 
< 	
IdentityResult	 
> 
UpdateUserAsync (
(( )
UserManager) 4
<4 5
AppUser5 <
>< =
userManager> I
,I J
RoleManagerK V
<V W
AppRoleW ^
>^ _
roleManager` k
,k l
UserDtom t
useru y
)y z
;z {
} �
dD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\AccessModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
AccessModelDto 
{ 
public 

string 

TargetPage 
{ 
get "
;" #
set$ '
;' (
}) *
public 

string 
? 
TargetId 
{ 
get !
;! "
set# &
;& '
}( )
} �
\D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\BanDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
BanDto 
{ 
public 

string 
User 
{ 
get 
; 
set !
;! "
}# $
public 

string 
Duration 
{ 
get  
;  !
set" %
;% &
}' (
} �
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\BanDurationsDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
static 
class 
BanDurationsDto #
{ 
public 

static 
List 
< 
string 
> 
	Durations (
{) *
get+ .
;. /
}0 1
=2 3
[4 5
$str5 =
,= >
$str? F
,F G
$strH P
,P Q
$strR [
,[ \
$str] h
]h i
;i j
} �
bD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\CommentModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
CommentModel 
{ 
public 

Guid 
? 
Id 
{ 
get 
; 
set 
; 
}  !
public 

string 
? 
Name 
{ 
get 
; 
set "
;" #
}$ %
public		 

string		 
Body		 
{		 
get		 
;		 
set		 !
;		! "
}		# $
public 

List 
< 
CommentModel 
> 
ChildComments +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
=: ;
[< =
]= >
;> ?
} �
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\CommentModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
CommentModelDto 
{ 
public 

CommentModel 
Comment 
{  !
get" %
;% &
set' *
;* +
}, -
public 

Guid 
? 
ParentId 
{ 
get 
;  
set! $
;$ %
}& '
public		 

string		 
?		 
Action		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
}

 �
aD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\CustomerDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
CustomerDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
public		 

DateTime		 

BannedTill		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 �
dD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\GameDtoWrapper.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
GameDtoWrapper 
{ 
public 

GameModelDto 
Game 
{ 
get "
;" #
set$ '
;' (
}) *
public		 

Guid		 
	Publisher		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
public 

List 
< 
Guid 
> 
Genres 
{ 
get "
;" #
set$ '
;' (
}) *
public 

List 
< 
Guid 
> 
	Platforms 
{  !
get" %
;% &
set' *
;* +
}, -
} �
bD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\GameModelDto.cs
	namespace 	
	Gamestore
 
. 
Services 
. 
Models #
;# $
public 
record 
GameModelDto 
{ 
private 
string 
_key 
; 
public

 

Guid

 
?

 
Id

 
{

 
get

 
;

 
set

 
;

 
}

  !
public 

string 
? 
Key 
{ 
get 
{ 	
return 
_key 
; 
} 	
set 
{ 	
if 
( 
string 
. 
IsNullOrEmpty $
($ %
value% *
)* +
)+ ,
{ 
_key 
= %
AutoGenrateGameKeyHelpers 0
.0 1
GenerateGameKey1 @
(@ A
NameA E
)E F
;F G
} 
else 
{ 
_key 
= 
value 
; 
} 
} 	
} 
public   

string   
Name   
{   
get   
;   
set   !
;  ! "
}  # $
public"" 

double"" 
Price"" 
{"" 
get"" 
;"" 
set"" "
;""" #
}""$ %
public$$ 

int$$ 
UnitInStock$$ 
{$$ 
get$$  
;$$  !
set$$" %
;$$% &
}$$' (
public&& 

int&& 
Discontinued&& 
{&& 
get&& !
;&&! "
set&&# &
;&&& '
}&&( )
public(( 

string(( 
Description(( 
{(( 
get((  #
;((# $
set((% (
;((( )
}((* +
public** 

DateOnly** 
PublishDate** 
{**  !
get**" %
;**% &
set**' *
;*** +
}**, -
public,, 

PublisherModelDto,, 
?,, 
	Publisher,, '
{,,( )
get,,* -
;,,- .
set,,/ 2
;,,2 3
},,4 5
public.. 

List.. 
<.. 
GenreModelDto.. 
>.. 
?.. 
Genres..  &
{..' (
get..) ,
;.., -
set... 1
;..1 2
}..3 4
public00 

List00 
<00 
PlatformModelDto00  
>00  !
?00! "
	Platforms00# ,
{00- .
get00/ 2
;002 3
set004 7
;007 8
}009 :
public22 

virtual22 
bool22 
Equals22 
(22 
GameModelDto22 +
?22+ ,
other22- 2
)222 3
{33 
if44 

(44 
ReferenceEquals44 
(44 
this44  
,44  !
other44" '
)44' (
)44( )
{55 	
return66 
true66 
;66 
}77 	
if99 

(99 
other99 
is99 
null99 
)99 
{:: 	
return;; 
false;; 
;;; 
}<< 	
return>> 
Id>> 
==>> 
other>> 
.>> 
Id>> 
&&>>  
Key>>! $
==>>% '
other>>( -
.>>- .
Key>>. 1
;>>1 2
}?? 
publicAA 

overrideAA 
intAA 
GetHashCodeAA #
(AA# $
)AA$ %
{BB 
returnCC 
HashCodeCC 
.CC 
CombineCC 
(CC  
IdCC  "
,CC" #
NameCC$ (
,CC( )
PriceCC* /
)CC/ 0
;CC0 1
}DD 
}EE �
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\GenreDtoWrapper.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
GenreDtoWrapper 
{ 
public 

GenreModelDto 
Genre 
{  
get! $
;$ %
set& )
;) *
}+ ,
} �
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\GenreModelDto.cs
	namespace 	
	Gamestore
 
. 
Services 
. 
Models #
;# $
public 
record 
GenreModelDto 
{ 
public 

Guid 
? 
Id 
{ 
get 
; 
set 
; 
}  !
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
public		 

Guid		 
?		 
ParentGenreId		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 �
`D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\LoginModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 

LoginModel 
{ 
public 

string 
Login 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
Password 
{ 
get  
;  !
set" %
;% &
}' (
public		 

bool		 
InternalAuth		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
}

 �
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\LoginModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
LoginModelDto 
{ 
public 


LoginModel 
Model 
{ 
get !
;! "
set# &
;& '
}( )
} �
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\MongoOrderDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
MongoOrderDto 
{ 
public 

int 
OrderId 
{ 
get 
; 
set !
;! "
}# $
public 

int 

CustomerId 
{ 
get 
;  
set! $
;$ %
}& '
public		 

int		 

EmployeeId		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
public 

DateTime 
	OrderDate 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
RequireDate 
{  !
get" %
;% &
set' *
;* +
}, -
public 

DateTime 
ShippedDate 
{  !
get" %
;% &
set' *
;* +
}, -
public 

int 
ShipVia 
{ 
get 
; 
set !
;! "
}# $
public 

double 
Freight 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
ShipName 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
ShipAddress 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
ShipCity 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
? 

ShipRegion 
{ 
get  #
;# $
set% (
;( )
}* +
public 

int 
ShipPostalCode 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
ShipCountry 
{ 
get  #
;# $
set% (
;( )
}* +
}   �	
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\MongoOrderModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
class 
MongoOrderModel 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public		 

string		 

CustomerId		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public 

OrderStatus 
Status 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
Date 
{ 
get 
; 
set  #
;# $
}% &
public 

List 
< 
OrderGameModelDto !
>! "

OrderGames# -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} �
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\OrderDetailsDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
OrderDetailsDto 
{ 
public 

Guid 
	ProductId 
{ 
get 
;  
set! $
;$ %
}& '
public 

double 
Price 
{ 
get 
; 
set "
;" #
}$ %
public		 

int		 
Quantity		 
{		 
get		 
;		 
set		 "
;		" #
}		$ %
public 

int 
Discount 
{ 
get 
; 
set "
;" #
}$ %
} �	
gD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\OrderGameModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
OrderGameModelDto 
{ 
public 

Guid 
OrderId 
{ 
get 
; 
set "
;" #
}$ %
public		 

Guid		 
	ProductId		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
public 

double 
Price 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
Quantity 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
? 
Discount 
{ 
get 
; 
set  #
;# $
}% &
public 

GameModelDto 
Product 
{  !
get" %
;% &
set' *
;* +
}, -
} �	
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\OrderModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
OrderModelDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public		 

string		 

CustomerId		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public 

OrderStatus 
Status 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
Date 
{ 
get 
; 
set !
;! "
}# $
public 

List 
< 
OrderGameModelDto !
>! "

OrderGames# -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} �
nD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\Payment\IboxPaymentModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
. 
Payment &
;& '
public 
record 
IboxPaymentModel 
{ 
public 

Guid 
? 
AccountNumber 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

Guid 
? 
InvoiceNumber 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 

decimal		 
?		 
TransactionAmount		 %
{		& '
get		( +
;		+ ,
set		- 0
;		0 1
}		2 3
}

 �
kD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\Payment\PaymentMethod.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
. 
Payment &
;& '
public 
record 
PaymentMethod 
{ 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
Description 
{ 
get  #
;# $
set% (
;( )
}* +
public		 

string		 
ImageUrl		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
}

 �
oD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\Payment\PaymentMethodsDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
. 
Payment &
;& '
public 
record 
PaymentMethodsDto 
{ 
public 

List 
< 
PaymentMethod 
> 
PaymentMethods -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} �
mD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\Payment\PaymentModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
. 
Payment &
;& '
public 
record 
PaymentModelDto 
{ 
public 

string 
Method 
{ 
get 
; 
set  #
;# $
}% &
public 

VisaPaymentModel 
? 
Model "
{# $
get% (
;( )
set* -
;- .
}/ 0
} �

zD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\Payment\VisaMicroservicePaymentModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
. 
Payment &
;& '
internal 
record	 (
VisaMicroservicePaymentModel ,
{ 
public 

string 
CardHolderName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

string 

CardNumber 
{ 
get "
;" #
set$ '
;' (
}) *
public		 

int		 
ExpirationMonth		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public 

int 
ExpirationYear 
{ 
get  #
;# $
set% (
;( )
}* +
public 

int 
Cvv 
{ 
get 
; 
set 
; 
}  
public 

double 
TransactionAmount #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} �

nD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\Payment\VisaPaymentModel.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
. 
Payment &
;& '
public 
record 
VisaPaymentModel 
{ 
public 

string 
Holder 
{ 
get 
; 
set  #
;# $
}% &
public 

string 

CardNumber 
{ 
get "
;" #
set$ '
;' (
}) *
public		 

int		 
MonthExpire		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public 

int 

YearExpire 
{ 
get 
;  
set! $
;$ %
}& '
public 

int 
Cvv2 
{ 
get 
; 
set 
; 
}  !
public 

double 
TransactionAmount #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} �
hD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\PlatformDtoWrapper.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
PlatformDtoWrapper  
{ 
public 

PlatformModelDto 
Platform $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} �
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\PlatformModelDto.cs
	namespace 	
	Gamestore
 
. 
Services 
. 
Models #
;# $
public 
record 
PlatformModelDto 
{ 
public 

Guid 
? 
Id 
{ 
get 
; 
set 
; 
}  !
public 

string 
Type 
{ 
get 
; 
set !
;! "
}# $
} �
iD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\PublisherDtoWrapper.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
PublisherDtoWrapper !
{ 
public 

PublisherModelDto 
	Publisher &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
} �
gD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\PublisherModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
PublisherModelDto 
{ 
public 

Guid 
? 
Id 
{ 
get 
; 
set 
; 
}  !
public 

string 
CompanyName 
{ 
get  #
;# $
set% (
;( )
}* +
public		 

string		 
?		 
HomePage		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
} �
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Models\ShipperModelDto.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Models 
; 
public 
record 
ShipperModelDto 
{ 
public 

int 
	ShipperID 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
CompanyName 
{ 
get  #
;# $
set% (
;( )
}* +
public		 

string		 
Phone		 
{		 
get		 
;		 
set		 "
;		" #
}		$ %
}

 �
pD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\MongoLogging\IMongoLoggingService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
MongoLogging $
;$ %
public 
	interface  
IMongoLoggingService %
{ 
Task 
LogGameAddAsync	 
( 
GameDtoWrapper '
value( -
)- .
;. /
Task

 
LogGameDeleteAsync

	 
(

 
Guid

  
gameId

! '
)

' (
;

( )
Task 
LogGameUpdateAsync	 
( 
GameModelDto (
oldValue) 1
,1 2
GameDtoWrapper3 A
newValueB J
)J K
;K L
} �c
oD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\MongoLogging\MongoLoggingService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
MongoLogging $
;$ %
public

 
class

 
MongoLoggingService

  
(

  !
IUnitOfWork

! ,

unitOfWork

- 7
,

7 8
IMongoUnitOfWork

9 I
mongoUnitOfWork

J Y
)

Y Z
:

[ \ 
IMongoLoggingService

] q
{ 
private 
readonly 
string 
_gameEntityType +
=, -
$str. 4
;4 5
private 
readonly 
string 
_updateAction )
=* +
$str, 4
;4 5
private 
readonly 
string 

_addAction &
=' (
$str) .
;. /
private 
readonly 
string 
_deleteAction )
=* +
$str, 4
;4 5
public 

async 
Task 
LogGameUpdateAsync (
(( )
GameModelDto) 5
oldValue6 >
,> ?
GameDtoWrapper@ N
newValueO W
)W X
{ 
List 
< 

MongoGenre 
> 
	oldGenres "
=# $
[% &
]& '
;' (
List 
< 

MongoGenre 
> 
	newGenres "
=# $
[% &
]& '
;' (
List 
< 
MongoPlatform 
> 
oldPlatforms (
=) *
[+ ,
], -
;- .
List 
< 
MongoPlatform 
> 
newPlatforms (
=) *
[+ ,
], -
;- .
var 
oldPublisherId 
= 
oldValue %
.% &
	Publisher& /
./ 0
Id0 2
.2 3
ToString3 ;
(; <
)< =
;= >
var 
newPublisherId 
= 
newValue %
.% &
	Publisher& /
./ 0
ToString0 8
(8 9
)9 :
;: ;
AddGenresToList 
( 
oldValue  
,  !
	oldGenres" +
)+ ,
;, -
await 
AddGenresToList 
( 

unitOfWork (
,( )
newValue* 2
,2 3
	newGenres4 =
)= >
;> ?
AddPlatformsToList   
(   
oldValue   #
,  # $
oldPlatforms  % 1
)  1 2
;  2 3
await!! 
AddPlatformsToList!!  
(!!  !

unitOfWork!!! +
,!!+ ,
newValue!!- 5
,!!5 6
newPlatforms!!7 C
)!!C D
;!!D E
var## 
mongoLogEntry## 
=## 
new## 
GameUpdateLogEntry##  2
(##2 3
)##3 4
{$$ 	
Date%% 
=%% 
DateTime%% 
.%% 
Now%% 
,%%  
Action&& 
=&& 
$"&& 
{&& 
_updateAction&& %
}&&% &
"&&& '
,&&' (

EntityType'' 
='' 
$"'' 
{'' 
_gameEntityType'' +
}''+ ,
"'', -
,''- .
NewValue(( 
=(( 
$"(( 
{(( 
oldValue(( "
}((" #
"((# $
,(($ %
OldValue)) 
=)) 
$")) 
{)) 
newValue)) "
}))" #
"))# $
,))$ %
OldPublisherId** 
=** 
oldPublisherId** +
!**+ ,
,**, -
	OldGenres++ 
=++ 
	oldGenres++ !
,++! "
OldPlatforms,, 
=,, 
oldPlatforms,, '
,,,' (
NewPublisherId-- 
=-- 
newPublisherId-- +
,--+ ,
	NewGenres.. 
=.. 
	newGenres.. !
,..! "
NewPlatforms// 
=// 
newPlatforms// '
,//' (
}00 	
;00	 

await22 
mongoUnitOfWork22 
.22 
LogRepository22 +
.22+ ,
LogGame22, 3
(223 4
mongoLogEntry224 A
)22A B
;22B C
}33 
public55 

async55 
Task55 
LogGameAddAsync55 %
(55% &
GameDtoWrapper55& 4
value555 :
)55: ;
{66 
List77 
<77 

MongoGenre77 
>77 
genres77 
=77  !
[77" #
]77# $
;77$ %
List88 
<88 
MongoPlatform88 
>88 
	platforms88 %
=88& '
[88( )
]88) *
;88* +
var:: 
publisherId:: 
=:: 
value:: 
.::  
	Publisher::  )
.::) *
ToString::* 2
(::2 3
)::3 4
;::4 5
await<< 
AddGenresToList<< 
(<< 

unitOfWork<< (
,<<( )
value<<* /
,<</ 0
genres<<1 7
)<<7 8
;<<8 9
await== 
AddPlatformsToList==  
(==  !

unitOfWork==! +
,==+ ,
value==- 2
,==2 3
	platforms==4 =
)=== >
;==> ?
GameAddLogEntry?? 
mongoLogEntry?? %
=??& '
new??( +
GameAddLogEntry??, ;
(??; <
)??< =
{@@ 	
DateAA 
=AA 
DateTimeAA 
.AA 
NowAA 
,AA  
ActionBB 
=BB 
$"BB 
{BB 

_addActionBB "
}BB" #
"BB# $
,BB$ %

EntityTypeCC 
=CC 
$"CC 
{CC 
_gameEntityTypeCC +
}CC+ ,
"CC, -
,CC- .
ValueDD 
=DD 
$"DD 
{DD 
valueDD 
}DD 
"DD 
,DD 
PublisherIdEE 
=EE 
publisherIdEE %
!EE% &
,EE& '
GenresFF 
=FF 
genresFF 
,FF 
	PlatformsGG 
=GG 
	platformsGG !
,GG! "
}HH 	
;HH	 

awaitJJ 
mongoUnitOfWorkJJ 
.JJ 
LogRepositoryJJ +
.JJ+ ,
LogGameJJ, 3
(JJ3 4
mongoLogEntryJJ4 A
)JJA B
;JJB C
}KK 
publicMM 

asyncMM 
TaskMM 
LogGameDeleteAsyncMM (
(MM( )
GuidMM) -
gameIdMM. 4
)MM4 5
{NN 
varOO 
gameOO 
=OO 
awaitOO 

unitOfWorkOO #
.OO# $
GameRepositoryOO$ 2
.OO2 3
GetByIdAsyncOO3 ?
(OO? @
gameIdOO@ F
)OOF G
;OOG H
GameDeleteLogEntryQQ 
mongoLogEntryQQ (
=QQ) *
newQQ+ .
GameDeleteLogEntryQQ/ A
(QQA B
)QQB C
{RR 	
DateSS 
=SS 
DateTimeSS 
.SS 
NowSS 
,SS  
ActionTT 
=TT 
$"TT 
{TT 
_deleteActionTT %
}TT% &
"TT& '
,TT' (

EntityTypeUU 
=UU 
$"UU 
{UU 
_gameEntityTypeUU +
}UU+ ,
"UU, -
,UU- .
ValueVV 
=VV 
$"VV 
{VV 
gameVV 
}VV 
"VV 
,VV 
}WW 	
;WW	 

awaitYY 
mongoUnitOfWorkYY 
.YY 
LogRepositoryYY +
.YY+ ,
LogGameYY, 3
(YY3 4
mongoLogEntryYY4 A
)YYA B
;YYB C
}ZZ 
private\\ 
static\\ 
void\\ 
AddGenresToList\\ '
(\\' (
GameModelDto\\( 4
source\\5 ;
,\\; <
List\\= A
<\\A B

MongoGenre\\B L
>\\L M
destination\\N Y
)\\Y Z
{]] 
foreach^^ 
(^^ 
var^^ 
genre^^ 
in^^ 
source^^ $
.^^$ %
Genres^^% +
)^^+ ,
{__ 	
destination`` 
.`` 
Add`` 
(`` 
new`` 
(``  
)``  !
{``" #
Id``$ &
=``' (
genre``) .
.``. /
Id``/ 1
.``1 2
ToString``2 :
(``: ;
)``; <
!``< =
,``= >
ParentGenreId``? L
=``M N
genre``O T
.``T U
ParentGenreId``U b
.``b c
ToString``c k
(``k l
)``l m
,``m n
Name``o s
=``t u
genre``v {
.``{ |
Name	``| �
}
``� �
)
``� �
;
``� �
}aa 	
}bb 
privatedd 
staticdd 
asyncdd 
Taskdd 
AddGenresToListdd -
(dd- .
IUnitOfWorkdd. 9

unitOfWorkdd: D
,ddD E
GameDtoWrapperddF T
sourceddU [
,dd[ \
Listdd] a
<dda b

MongoGenreddb l
>ddl m
destinationddn y
)ddy z
{ee 
foreachff 
(ff 
varff 
genreIdff 
inff 
sourceff  &
.ff& '
Genresff' -
)ff- .
{gg 	
varhh 
namehh 
=hh 
(hh 
awaithh 

unitOfWorkhh (
.hh( )
GenreRepositoryhh) 8
.hh8 9
GetByIdAsynchh9 E
(hhE F
genreIdhhF M
)hhM N
)hhN O
.hhO P
NamehhP T
;hhT U
destinationii 
.ii 
Addii 
(ii 
newii 
(ii  
)ii  !
{ii" #
Idii$ &
=ii' (
genreIdii) 0
.ii0 1
ToStringii1 9
(ii9 :
)ii: ;
,ii; <
Nameii= A
=iiB C
nameiiD H
}iiI J
)iiJ K
;iiK L
}jj 	
}kk 
privatemm 
staticmm 
voidmm 
AddPlatformsToListmm *
(mm* +
GameModelDtomm+ 7
sourcemm8 >
,mm> ?
Listmm@ D
<mmD E
MongoPlatformmmE R
>mmR S
destinationmmT _
)mm_ `
{nn 
foreachoo 
(oo 
varoo 
platformoo 
inoo  
sourceoo! '
.oo' (
	Platformsoo( 1
)oo1 2
{pp 	
destinationqq 
.qq 
Addqq 
(qq 
newqq 
(qq  
)qq  !
{qq" #
Idqq$ &
=qq' (
platformqq) 1
.qq1 2
Idqq2 4
.qq4 5
ToStringqq5 =
(qq= >
)qq> ?
!qq? @
,qq@ A
TypeqqB F
=qqG H
platformqqI Q
.qqQ R
TypeqqR V
}qqW X
)qqX Y
;qqY Z
}rr 	
}ss 
privateuu 
staticuu 
asyncuu 
Taskuu 
AddPlatformsToListuu 0
(uu0 1
IUnitOfWorkuu1 <

unitOfWorkuu= G
,uuG H
GameDtoWrapperuuI W
sourceuuX ^
,uu^ _
Listuu` d
<uud e
MongoPlatformuue r
>uur s
destinationuut 
)	uu �
{vv 
foreachww 
(ww 
varww 

platformIdww 
inww  "
sourceww# )
.ww) *
	Platformsww* 3
)ww3 4
{xx 	
varyy 
typeyy 
=yy 
(yy 
awaityy 

unitOfWorkyy (
.yy( )
PlatformRepositoryyy) ;
.yy; <
GetByIdAsyncyy< H
(yyH I

platformIdyyI S
)yyS T
)yyT U
.yyU V
TypeyyV Z
;yyZ [
destinationzz 
.zz 
Addzz 
(zz 
newzz 
(zz  
)zz  !
{zz" #
Idzz$ &
=zz' (

platformIdzz) 3
.zz3 4
ToStringzz4 <
(zz< =
)zz= >
,zz> ?
Typezz@ D
=zzE F
typezzG K
}zzL M
)zzM N
;zzN O
}{{ 	
}|| 
}}} �	
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\CommentService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
public		 
class		 
CommentService		 
(		 
IBanService		 '

banService		( 2
)		2 3
:		4 5
ICommentService		6 E
{

 
public 

async 
Task *
BanCustomerFromCommentingAsync 4
(4 5
BanDto5 ;

banDetails< F
,F G
UserManagerH S
<S T
AppUserT [
>[ \
userManager] h
)h i
{ 
await 

banService 
. *
BanCustomerFromCommentingAsync 7
(7 8

banDetails8 B
,B C
userManagerD O
)O P
;P Q
} 
public 

List 
< 
string 
> 
GetBanDurations '
(' (
)( )
{ 
return 
BanDurationsDto 
. 
	Durations (
;( )
} 
} ��
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\GameService.cs
	namespace 	
	Gamestore
 
. 
Services 
. 
Services %
;% &
public 
class 
GameService 
( 
IUnitOfWork 

unitOfWork 
, 
IMongoUnitOfWork 
mongoUnitOfWork $
,$ %
IMapper 

automapper 
, 
ILogger 
< 
GameService 
> 
logger 
,   
IMongoLoggingService 
mongoLoggingService ,
,, -+
IGameProcessingPipelineDirector #*
gameProcessingPipelineDirector$ B
)B C
:D E
IGameServiceF R
{   
private!! 
const!! 
string!! 
QuoteActionName!! (
=!!) *
$str!!+ 2
;!!2 3
private"" 
const"" 
string"" "
DeletedMessageTemplate"" /
=""0 1
$str""2 O
;""O P
private## 
readonly## #
GameDtoWrapperValidator## ,$
_gameDtoWrapperValidator##- E
=##F G
new##H K
(##K L
)##L M
;##M N
private$$ 
readonly$$ $
CommentModelDtoValidator$$ -%
_commentModelDtoValidator$$. G
=$$H I
new$$J M
($$M N
)$$N O
;$$O P
public&& 

async&& 
Task&& 
<&& 
List&& 
<&& 
GameModelDto&& '
>&&' (
>&&( )
GetAllGamesAsync&&* :
(&&: ;
bool&&; ?
canSeeDeletedGames&&@ R
)&&R S
{'' 
logger(( 
.(( 
LogInformation(( 
((( 
$str(( 1
)((1 2
;((2 3
List** 
<** 
GameModelDto** 
>** 

gameModels** %
;**% &
if++ 

(++ 
canSeeDeletedGames++ 
)++ 
{,, 	

gameModels-- 
=-- 

automapper-- #
.--# $
Map--$ '
<--' (
List--( ,
<--, -
GameModelDto--- 9
>--9 :
>--: ;
(--; <
await--< A

unitOfWork--B L
.--L M
GameRepository--M [
.--[ \"
GetAllWithDeletedAsync--\ r
(--r s
)--s t
)--t u
;--u v
}.. 	
else// 
{00 	

gameModels11 
=11 

automapper11 #
.11# $
Map11$ '
<11' (
List11( ,
<11, -
GameModelDto11- 9
>119 :
>11: ;
(11; <
await11< A

unitOfWork11B L
.11L M
GameRepository11M [
.11[ \
GetAllAsync11\ g
(11g h
)11h i
)11i j
;11j k
}22 	
var44 
productsFromMongoDB44 
=44  !

automapper44" ,
.44, -
Map44- 0
<440 1
List441 5
<445 6
GameModelDto446 B
>44B C
>44C D
(44D E
await44E J
mongoUnitOfWork44K Z
.44Z [
ProductRepository44[ l
.44l m
GetAllAsync44m x
(44x y
)44y z
)44z {
;44{ |

gameModels55 
.55 
AddRange55 
(55 
productsFromMongoDB55 /
)55/ 0
;550 1
return77 

gameModels77 
;77 
}88 
public:: 

async:: 
Task:: 
<:: 
FilteredGamesDto:: &
>::& '!
GetFilteredGamesAsync::( =
(::= >
GameFiltersDto::> L
gameFilters::M X
,::X Y
bool::Z ^
canSeeDeletedGames::_ q
)::q r
{;; 
logger<< 
.<< 
LogInformation<< 
(<< 
$str<< 7
)<<7 8
;<<8 9
var>> )
gameProcessingPipelineService>> )
=>>* +*
gameProcessingPipelineDirector>>, J
.>>J K2
&ConstructGameCollectionPipelineService>>K q
(>>q r
)>>r s
;>>s t
FilteredGamesDto@@ 
filteredGameDtos@@ )
=@@* +
new@@, /
(@@/ 0
)@@0 1
;@@1 2
awaitAA "
SqlServerHelperServiceAA $
.AA$ %)
FilterGamesFromSQLServerAsyncAA% B
(AAB C

unitOfWorkAAC M
,AAM N
mongoUnitOfWorkAAO ^
,AA^ _

automapperAA` j
,AAj k
gameFiltersAAl w
,AAw x
filteredGameDtos	AAy �
,
AA� �+
gameProcessingPipelineService
AA� �
,
AA� � 
canSeeDeletedGames
AA� �
)
AA� �
;
AA� �
awaitBB  
MongoDbHelperServiceBB "
.BB" #*
FilterProductsFromMongoDBAsyncBB# A
(BBA B

unitOfWorkBBB L
,BBL M
mongoUnitOfWorkBBN ]
,BB] ^

automapperBB_ i
,BBi j
gameFiltersBBk v
,BBv w
filteredGameDtos	BBx �
,
BB� �+
gameProcessingPipelineService
BB� �
)
BB� �
;
BB� �/
#SetTotalNumberOfPagesAfterFilteringCC +
(CC+ ,
gameFiltersCC, 7
,CC7 8
filteredGameDtosCC9 I
)CCI J
;CCJ K<
0CheckIfCurrentPageDoesntExceedTotalNumberOfPagesDD 8
(DD8 9
gameFiltersDD9 D
,DDD E
filteredGameDtosDDF V
)DDV W
;DDW X
returnFF 
filteredGameDtosFF 
;FF  
}GG 
publicII 

asyncII 
TaskII 
<II 
IEnumerableII !
<II! "
GenreModelDtoII" /
>II/ 0
>II0 1#
GetGenresByGameKeyAsyncII2 I
(III J
stringIIJ P
gameKeyIIQ X
)IIX Y
{JJ 
loggerKK 
.KK 
LogInformationKK 
(KK 
$strKK D
,KKD E
gameKeyKKF M
)KKM N
;KKN O
varMM 
genreModelsMM 
=MM 
awaitMM "
SqlServerHelperServiceMM  6
.MM6 70
$GetGenresFromSQLServerByGameKeyAsyncMM7 [
(MM[ \

unitOfWorkMM\ f
,MMf g

automapperMMh r
,MMr s
gameKeyMMt {
)MM{ |
;MM| }
genreModelsNN 
??=NN 
awaitNN  
MongoDbHelperServiceNN 2
.NN2 3.
"GetGenresFromMongoDBByGameKeyAsyncNN3 U
(NNU V
mongoUnitOfWorkNNV e
,NNe f

automapperNNg q
,NNq r
gameKeyNNs z
)NNz {
;NN{ |
returnPP 
genreModelsPP 
.PP 
AsEnumerablePP '
(PP' (
)PP( )
;PP) *
}QQ 
publicSS 

asyncSS 
TaskSS 
<SS 
IEnumerableSS !
<SS! "
PlatformModelDtoSS" 2
>SS2 3
>SS3 4&
GetPlatformsByGameKeyAsyncSS5 O
(SSO P
stringSSP V
gameKeySSW ^
)SS^ _
{TT 
loggerUU 
.UU 
LogInformationUU 
(UU 
$strUU H
,UUH I
gameKeyUUJ Q
)UUQ R
;UUR S
varVV 
gameVV 
=VV 
awaitVV 

unitOfWorkVV #
.VV# $
GameRepositoryVV$ 2
.VV2 3
GetGameByKeyAsyncVV3 D
(VVD E
gameKeyVVE L
)VVL M
;VVM N
ifWW 

(WW 
gameWW 
isWW 
notWW 
nullWW 
)WW 
{XX 	
varYY 
	platformsYY 
=YY 
awaitYY !

unitOfWorkYY" ,
.YY, -
GameRepositoryYY- ;
.YY; <#
GetPlatformsByGameAsyncYY< S
(YYS T
gameYYT X
.YYX Y
IdYYY [
)YY[ \
;YY\ ]
if[[ 
([[ 
	platforms[[ 
.[[ 
IsNullOrEmpty[[ '
([[' (
)[[( )
)[[) *
{\\ 
return]] 
[]] 
]]] 
;]] 
}^^ 
var`` 
platformModels`` 
=``  

automapper``! +
.``+ ,
Map``, /
<``/ 0
List``0 4
<``4 5
PlatformModelDto``5 E
>``E F
>``F G
(``G H
	platforms``H Q
)``Q R
;``R S
returnbb 
platformModelsbb !
.bb! "
AsEnumerablebb" .
(bb. /
)bb/ 0
;bb0 1
}cc 	
returnee 
[ee 
]ee 
;ee 
}ff 
publichh 

asynchh 
Taskhh 
<hh 
PublisherModelDtohh '
>hh' (&
GetPublisherByGameKeyAsynchh) C
(hhC D
stringhhD J
gameKeyhhK R
)hhR S
{ii 
loggerjj 
.jj 
LogInformationjj 
(jj 
$strjj H
,jjH I
gameKeyjjJ Q
)jjQ R
;jjR S
varll 
	publisherll 
=ll 
awaitll "
SqlServerHelperServicell 4
.ll4 53
'GetPublisherFromSQLServerByGameKeyAsyncll5 \
(ll\ ]

unitOfWorkll] g
,llg h

automapperlli s
,lls t
gameKeyllu |
)ll| }
;ll} ~
	publishermm 
??=mm 
awaitmm  
MongoDbHelperServicemm 0
.mm0 11
%GetPublisherFromMongoDBByGameKeyAsyncmm1 V
(mmV W
mongoUnitOfWorkmmW f
,mmf g

automappermmh r
,mmr s
gameKeymmt {
)mm{ |
;mm| }
returnoo 
	publisheroo 
;oo 
}pp 
publicrr 

asyncrr 
Taskrr 
<rr 
GameModelDtorr "
>rr" #
GetGameByIdAsyncrr$ 4
(rr4 5
Guidrr5 9
gameIdrr: @
)rr@ A
{ss 
loggertt 
.tt 
LogInformationtt 
(tt 
$strtt <
,tt< =
gameIdtt> D
)ttD E
;ttE F
varuu 
gameuu 
=uu 
awaituu "
SqlServerHelperServiceuu /
.uu/ 0)
GetGameFromSQLServerByIdAsyncuu0 M
(uuM N

unitOfWorkuuN X
,uuX Y
gameIduuZ `
)uu` a
;uua b
gameww 
??=ww 
awaitww  
MongoDbHelperServiceww +
.ww+ ,'
GetGameFromMongoDBByIdAsyncww, G
(wwG H
mongoUnitOfWorkwwH W
,wwW X

automapperwwY c
,wwc d
gameIdwwe k
)wwk l
;wwl m
returnyy 
gameyy 
==yy 
nullyy 
?yy 
throwyy #
newyy$ '
GamestoreExceptionyy( :
(yy: ;
$"yy; =
$stryy= Z
{yyZ [
gameIdyy[ a
}yya b
"yyb c
)yyc d
:yye f

automapperyyg q
.yyq r
Mapyyr u
<yyu v
GameModelDto	yyv �
>
yy� �
(
yy� �
game
yy� �
)
yy� �
;
yy� �
}zz 
public|| 

async|| 
Task|| 
<|| 
GameModelDto|| "
>||" #
GetGameByKeyAsync||$ 5
(||5 6
string||6 <
key||= @
)||@ A
{}} 
logger~~ 
.~~ 
LogInformation~~ 
(~~ 
$str~~ :
,~~: ;
key~~< ?
)~~? @
;~~@ A
var
�� 
game
�� 
=
�� 
await
�� $
SqlServerHelperService
�� /
.
��/ 0,
GetGameFromSQLServerByKeyAsync
��0 N
(
��N O

unitOfWork
��O Y
,
��Y Z

automapper
��[ e
,
��e f
key
��g j
)
��j k
;
��k l
game
�� 
??=
�� 
await
�� "
MongoDbHelperService
�� +
.
��+ ,5
'GetGameWithDetailsFromMongoDBByKeyAsync
��, S
(
��S T
mongoUnitOfWork
��T c
,
��c d

automapper
��e o
,
��o p
key
��q t
)
��t u
;
��u v
return
�� 
game
�� 
??
�� 
throw
�� 
new
��   
GamestoreException
��! 3
(
��3 4
$"
��4 6
$str
��6 T
{
��T U
key
��U X
}
��X Y
"
��Y Z
)
��Z [
;
��[ \
}
�� 
public
�� 

List
�� 
<
�� 
string
�� 
>
�� "
GetPaginationOptions
�� ,
(
��, -
)
��- .
{
�� 
return
�� "
PaginationOptionsDto
�� #
.
��# $
PaginationOptions
��$ 5
;
��5 6
}
�� 
public
�� 

List
�� 
<
�� 
string
�� 
>
�� #
GetPublishDateOptions
�� -
(
��- .
)
��. /
{
�� 
return
�� #
PublishDateOptionsDto
�� $
.
��$ % 
PublishDateOptions
��% 7
;
��7 8
}
�� 
public
�� 

List
�� 
<
�� 
string
�� 
>
�� 
GetSortingOptions
�� )
(
��) *
)
��* +
{
�� 
return
�� 
SortingOptionsDto
��  
.
��  !
SortingOptions
��! /
;
��/ 0
}
�� 
public
�� 

async
�� 
Task
�� 
AddGameAsync
�� "
(
��" #
GameDtoWrapper
��# 1
	gameModel
��2 ;
)
��; <
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� 8
,
��8 9
	gameModel
��: C
)
��C D
;
��D E
await
�� &
_gameDtoWrapperValidator
�� &
.
��& '
ValidateGame
��' 3
(
��3 4
	gameModel
��4 =
)
��= >
;
��> ?
var
�� 
game
�� 
=
�� 

automapper
�� 
.
�� 
Map
�� !
<
��! "
Game
��" &
>
��& '
(
��' (
	gameModel
��( 1
.
��1 2
Game
��2 6
)
��6 7
;
��7 8
var
�� 
	addedGame
�� 
=
�� 
await
�� &
AddGameToRepositoryAsync
�� 6
(
��6 7

unitOfWork
��7 A
,
��A B
	gameModel
��C L
,
��L M
game
��N R
)
��R S
;
��S T
var
�� 
genres
�� 
=
�� 
	gameModel
�� 
.
�� 
Genres
�� %
;
��% &
var
�� 
	platforms
�� 
=
�� 
	gameModel
�� !
.
��! "
	Platforms
��" +
;
��+ ,
await
�� ,
AddGameGenresTorepositoryAsync
�� ,
(
��, -

unitOfWork
��- 7
,
��7 8
	addedGame
��9 B
,
��B C
genres
��D J
)
��J K
;
��K L
await
�� /
!AddGamePlatformsToRepositoryAsync
�� /
(
��/ 0

unitOfWork
��0 :
,
��: ;
	addedGame
��< E
,
��E F
	platforms
��G P
)
��P Q
;
��Q R
await
�� !
mongoLoggingService
�� !
.
��! "
LogGameAddAsync
��" 1
(
��1 2
	gameModel
��2 ;
)
��; <
;
��< =
}
�� 
public
�� 

async
�� 
Task
�� 
UpdateGameAsync
�� %
(
��% &
GameDtoWrapper
��& 4
	gameModel
��5 >
)
��> ?
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� :
,
��: ;
	gameModel
��< E
)
��E F
;
��F G
await
�� &
_gameDtoWrapperValidator
�� &
.
��& '
ValidateGame
��' 3
(
��3 4
	gameModel
��4 =
)
��= >
;
��> ?
GameModelDto
�� 
oldObjectState
�� #
;
��# $
GameDtoWrapper
�� 
newObjectState
�� %
=
��& '
	gameModel
��( 1
;
��1 2
var
�� %
existingGameInSQLServer
�� #
=
��$ %
await
��& +

unitOfWork
��, 6
.
��6 7
GameRepository
��7 E
.
��E F
GetByIdAsync
��F R
(
��R S
(
��S T
Guid
��T X
)
��X Y
	gameModel
��Y b
.
��b c
Game
��c g
.
��g h
Id
��h j
!
��j k
)
��k l
;
��l m
if
�� 

(
�� %
existingGameInSQLServer
�� #
!=
��$ &
null
��' +
)
��+ ,
{
�� 	
oldObjectState
�� 
=
�� 

automapper
�� '
.
��' (
Map
��( +
<
��+ ,
GameModelDto
��, 8
>
��8 9
(
��9 :%
existingGameInSQLServer
��: Q
)
��Q R
;
��R S
}
�� 	
else
�� 
{
�� 	
var
�� 
id
�� 
=
�� 
GuidHelpers
��  
.
��  !
	GuidToInt
��! *
(
��* +
(
��+ ,
Guid
��, 0
)
��0 1
	gameModel
��1 :
.
��: ;
Game
��; ?
.
��? @
Id
��@ B
)
��B C
;
��C D
var
�� 
gameFromMongoDB
�� 
=
��  !
await
��" '"
MongoDbHelperService
��( <
.
��< =4
&GetGameWithDetailsFromMongoDBByIdAsync
��= c
(
��c d
mongoUnitOfWork
��d s
,
��s t

automapper
��u 
,�� �
id��� �
)��� �
;��� �
await
�� $
SqlServerHelperService
�� (
.
��( )C
5CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync
��) ^
(
��^ _

unitOfWork
��_ i
,
��i j

automapper
��k u
,
��u v
gameFromMongoDB��w �
,��� �'
existingGameInSQLServer��� �
)��� �
;��� �
oldObjectState
�� 
=
�� 
gameFromMongoDB
�� ,
;
��, -
}
�� 	
await
�� 1
#DeleteGameGenresFromRepositoryAsync
�� 1
(
��1 2

unitOfWork
��2 <
,
��< =
	gameModel
��> G
.
��G H
Game
��H L
.
��L M
Id
��M O
)
��O P
;
��P Q
await
�� 4
&DeleteGamePlatformsFromRepositoryAsync
�� 4
(
��4 5

unitOfWork
��5 ?
,
��? @
	gameModel
��A J
.
��J K
Game
��K O
.
��O P
Id
��P R
)
��R S
;
��S T
var
�� 
game
�� 
=
�� 

automapper
�� 
.
�� 
Map
�� !
<
��! "
Game
��" &
>
��& '
(
��' (
	gameModel
��( 1
.
��1 2
Game
��2 6
)
��6 7
;
��7 8
await
�� )
UpdateGameInrepositoryAsync
�� )
(
��) *

unitOfWork
��* 4
,
��4 5
	gameModel
��6 ?
,
��? @
game
��A E
)
��E F
;
��F G
await
�� !
mongoLoggingService
�� !
.
��! " 
LogGameUpdateAsync
��" 4
(
��4 5
oldObjectState
��5 C
,
��C D
newObjectState
��E S
)
��S T
;
��T U
}
�� 
public
�� 

async
�� 
Task
�� !
DeleteGameByIdAsync
�� )
(
��) *
Guid
��* .
gameId
��/ 5
)
��5 6
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� =
,
��= >
gameId
��? E
)
��E F
;
��F G
var
�� 
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetByIdAsync
��3 ?
(
��? @
gameId
��@ F
)
��F G
;
��G H
if
�� 

(
�� 
game
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
await
�� 1
#DeleteOrderGamesFromRepositoryAsync
�� 5
(
��5 6

unitOfWork
��6 @
,
��@ A
game
��B F
)
��F G
;
��G H
await
�� 1
#DeleteGameGenresFromRepositoryAsync
�� 5
(
��5 6

unitOfWork
��6 @
,
��@ A
game
��B F
.
��F G
Id
��G I
)
��I J
;
��J K
await
�� 4
&DeleteGamePlatformsFromRepositoryAsync
�� 8
(
��8 9

unitOfWork
��9 C
,
��C D
game
��E I
.
��I J
Id
��J L
)
��L M
;
��M N
await
�� +
DeleteGameFromRepositoryAsync
�� /
(
��/ 0

unitOfWork
��0 :
,
��: ;
game
��< @
)
��@ A
;
��A B
}
�� 	
else
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$"
��) +
$str
��+ H
{
��H I
gameId
��I O
}
��O P
"
��P Q
)
��Q R
;
��R S
}
�� 	
}
�� 
public
�� 

async
�� 
Task
�� %
SoftDeleteGameByIdAsync
�� -
(
��- .
Guid
��. 2
gameId
��3 9
)
��9 :
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� =
,
��= >
gameId
��? E
)
��E F
;
��F G
var
�� 
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetByIdAsync
��3 ?
(
��? @
gameId
��@ F
)
��F G
;
��G H
if
�� 

(
�� 
game
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
await
�� /
!SoftDeleteGameFromRepositoryAsync
�� 3
(
��3 4

unitOfWork
��4 >
,
��> ?
game
��@ D
)
��D E
;
��E F
await
�� !
mongoLoggingService
�� %
.
��% & 
LogGameDeleteAsync
��& 8
(
��8 9
gameId
��9 ?
)
��? @
;
��@ A
}
�� 	
else
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$"
��) +
$str
��+ H
{
��H I
gameId
��I O
}
��O P
"
��P Q
)
��Q R
;
��R S
}
�� 	
}
�� 
public
�� 

async
�� 
Task
�� "
DeleteGameByKeyAsync
�� *
(
��* +
string
��+ 1
gameKey
��2 9
)
��9 :
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� ?
,
��? @
gameKey
��A H
)
��H I
;
��I J
var
�� 
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetGameByKeyAsync
��3 D
(
��D E
gameKey
��E L
)
��L M
;
��M N
if
�� 

(
�� 
game
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
await
�� 1
#DeleteOrderGamesFromRepositoryAsync
�� 5
(
��5 6

unitOfWork
��6 @
,
��@ A
game
��B F
)
��F G
;
��G H
await
�� 1
#DeleteGameGenresFromRepositoryAsync
�� 5
(
��5 6

unitOfWork
��6 @
,
��@ A
game
��B F
.
��F G
Id
��G I
)
��I J
;
��J K
await
�� 4
&DeleteGamePlatformsFromRepositoryAsync
�� 8
(
��8 9

unitOfWork
��9 C
,
��C D
game
��E I
.
��I J
Id
��J L
)
��L M
;
��M N
await
�� +
DeleteGameFromRepositoryAsync
�� /
(
��/ 0

unitOfWork
��0 :
,
��: ;
game
��< @
)
��@ A
;
��A B
}
�� 	
else
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$"
��) +
$str
��+ I
{
��I J
gameKey
��J Q
}
��Q R
"
��R S
)
��S T
;
��T U
}
�� 	
}
�� 
public
�� 

async
�� 
Task
�� &
SoftDeleteGameByKeyAsync
�� .
(
��. /
string
��/ 5
gameKey
��6 =
)
��= >
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� ?
,
��? @
gameKey
��A H
)
��H I
;
��I J
var
�� 
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetGameByKeyAsync
��3 D
(
��D E
gameKey
��E L
)
��L M
;
��M N
if
�� 

(
�� 
game
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
await
�� /
!SoftDeleteGameFromRepositoryAsync
�� 3
(
��3 4

unitOfWork
��4 >
,
��> ?
game
��@ D
)
��D E
;
��E F
}
�� 	
else
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$"
��) +
$str
��+ I
{
��I J
gameKey
��J Q
}
��Q R
"
��R S
)
��S T
;
��T U
}
�� 	
}
�� 
public
�� 

async
�� 
Task
��  
AddGameToCartAsync
�� (
(
��( )
Guid
��) -

customerId
��. 8
,
��8 9
string
��: @
gameKey
��A H
,
��H I
int
��J M
quantity
��N V
)
��V W
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� ?
,
��? @
gameKey
��A H
)
��H I
;
��I J
var
�� 
game
�� 
=
�� 
await
�� $
SqlServerHelperService
�� /
.
��/ 0,
GetGameFromSQLServerByKeyAsync
��0 N
(
��N O

unitOfWork
��O Y
,
��Y Z

automapper
��[ e
,
��e f
gameKey
��g n
)
��n o
;
��o p
game
�� 
??=
�� 
await
�� "
MongoDbHelperService
�� +
.
��+ ,5
'GetGameWithDetailsFromMongoDBByKeyAsync
��, S
(
��S T
mongoUnitOfWork
��T c
,
��c d

automapper
��e o
,
��o p
gameKey
��q x
)
��x y
??
��z |
throw��} �
new��� �"
GamestoreException��� �
(��� �
$"��� �
$str��� �
{��� �
gameKey��� �
}��� �
"��� �
)��� �
;��� �
var
�� 
unitInStock
�� 
=
�� 
game
�� 
.
�� 
UnitInStock
�� *
;
��* +
if
�� 

(
�� 
unitInStock
�� 
>
�� 
$num
�� 
)
�� 
{
�� 	
var
�� 
exisitngOrder
�� 
=
�� 
await
��  %

unitOfWork
��& 0
.
��0 1
OrderRepository
��1 @
.
��@ A"
GetByCustomerIdAsync
��A U
(
��U V

customerId
��V `
)
��` a
;
��a b
if
�� 
(
�� 
exisitngOrder
�� 
==
��  
null
��! %
||
��& (
exisitngOrder
��) 6
.
��6 7
Status
��7 =
!=
��> @
OrderStatus
��A L
.
��L M
Open
��M Q
)
��Q R
{
�� 
await
�� !
CreateNewOrderAsync
�� )
(
��) *

unitOfWork
��* 4
,
��4 5

automapper
��6 @
,
��@ A

customerId
��B L
,
��L M
quantity
��N V
,
��V W
game
��X \
,
��\ ]
unitInStock
��^ i
)
��i j
;
��j k
}
�� 
else
�� 
{
�� 
await
�� $
SqlServerHelperService
�� ,
.
��, -&
UpdateExistingOrderAsync
��- E
(
��E F

unitOfWork
��F P
,
��P Q

automapper
��R \
,
��\ ]
quantity
��^ f
,
��f g
game
��h l
,
��l m
unitInStock
��n y
,
��y z
exisitngOrder��{ �
)��� �
;��� �
}
�� 
}
�� 	
}
�� 
public
�� 

async
�� 
Task
�� 
<
�� 
IEnumerable
�� !
<
��! "
CommentModel
��" .
>
��. /
>
��/ 0'
GetCommentsByGameKeyAsync
��1 J
(
��J K
string
��K Q
gameKey
��R Y
)
��Y Z
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� H
,
��H I
gameKey
��J Q
)
��Q R
;
��R S
var
�� 
comments
�� 
=
�� 
await
�� 

unitOfWork
�� '
.
��' (
CommentRepository
��( 9
.
��9 :
GetByGameKeyAsync
��: K
(
��K L
gameKey
��L S
)
��S T
;
��T U
var
�� 
commentHelpers
�� 
=
�� 
new
��  
CommentHelpers
��! /
(
��/ 0

automapper
��0 :
)
��: ;
;
��; <
List
�� 
<
�� 
CommentModel
�� 
>
�� 
commentList
�� &
=
��' (
commentHelpers
��) 7
.
��7 8 
CommentListCreator
��8 J
(
��J K
comments
��K S
)
��S T
;
��T U
return
�� 
commentList
�� 
.
�� 
AsEnumerable
�� '
(
��' (
)
��( )
;
��) *
}
�� 
public
�� 

async
�� 
Task
�� 
<
�� 
string
�� 
>
�� #
AddCommentToGameAsync
�� 3
(
��3 4
string
��4 :
userName
��; C
,
��C D
string
��E K
gameKey
��L S
,
��S T
CommentModelDto
��U d
comment
��e l
,
��l m
UserManager
��n y
<
��y z
AppUser��z �
>��� �
userManager��� �
)��� �
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� M
,
��M N
comment
��O V
,
��V W
gameKey
��X _
)
��_ `
;
��` a
await
�� '
_commentModelDtoValidator
�� '
.
��' (
ValidateComment
��( 7
(
��7 8
comment
��8 ?
)
��? @
;
��@ A
var
�� 
banCheckResult
�� 
=
�� 
await
�� "!
CheckIfUserIsBanned
��# 6
(
��6 7
userName
��7 ?
,
��? @
userManager
��A L
)
��L M
;
��M N
if
�� 

(
�� 
banCheckResult
�� 
.
�� 
IsBanned
�� #
)
��# $
{
�� 	
return
�� 
$"
�� 
$str
�� &
{
��& '
banCheckResult
��' 5
.
��5 6

BannedTill
��6 @
}
��@ A
"
��A B
;
��B C
}
�� 	
else
�� 
{
�� 	
var
�� 
game
�� 
=
�� 
await
�� 

unitOfWork
�� '
.
��' (
GameRepository
��( 6
.
��6 7
GetGameByKeyAsync
��7 H
(
��H I
@gameKey
��I Q
)
��Q R
;
��R S
if
�� 
(
�� 
game
�� 
==
�� 
null
�� 
)
�� 
{
�� 
GameModelDto
�� 
gameModelDto
�� )
=
��* +

automapper
��, 6
.
��6 7
Map
��7 :
<
��: ;
GameModelDto
��; G
>
��G H
(
��H I
await
��I N
mongoUnitOfWork
��O ^
.
��^ _
ProductRepository
��_ p
.
��p q
GetByNameAsync
��q 
(�� �
gameKey��� �
)��� �
)��� �
;��� �
await
�� $
SqlServerHelperService
�� ,
.
��, -C
5CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync
��- b
(
��b c

unitOfWork
��c m
,
��m n

automapper
��o y
,
��y z
gameModelDto��{ �
,��� �
game��� �
)��� �
;��� �
}
�� 
comment
�� 
.
�� 
Comment
�� 
.
�� 
Name
��  
=
��! "
userName
��# +
;
��+ ,
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetGameByKeyAsync
��3 D
(
��D E
@gameKey
��E M
)
��M N
;
��N O
var
�� 
commenttoAdd
�� 
=
�� -
ConvertCommentModelDtoToComment
�� >
(
��> ?
comment
��? F
,
��F G
game
��H L
.
��L M
Id
��M O
)
��O P
;
��P Q
if
�� 
(
�� 
comment
�� 
.
�� 
Action
�� 
==
�� !
QuoteActionName
��" 1
&&
��2 4
comment
��5 <
.
��< =
ParentId
��= E
!=
��F H
null
��I M
)
��M N
{
�� "
ComposeQuotedMessage
�� $
(
��$ %
commenttoAdd
��% 1
)
��1 2
;
��2 3
}
�� 
await
�� )
AddMessageToRepositoryAsync
�� -
(
��- .

unitOfWork
��. 8
,
��8 9
commenttoAdd
��: F
)
��F G
;
��G H
}
�� 	
return
�� 
string
�� 
.
�� 
Empty
�� 
;
�� 
}
�� 
public
�� 

async
�� 
Task
��  
DeleteCommentAsync
�� (
(
��( )
string
��) /
userName
��0 8
,
��8 9
string
��: @
gameKey
��A H
,
��H I
Guid
��J N
	commentId
��O X
,
��X Y
bool
��Z ^
canModerate
��_ j
)
��j k
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� >
,
��> ?
	commentId
��@ I
)
��I J
;
��J K
var
�� 
comment
�� 
=
�� 
await
�� 

unitOfWork
�� &
.
��& '
CommentRepository
��' 8
.
��8 9
GetByIdAsync
��9 E
(
��E F
	commentId
��F O
)
��O P
;
��P Q
if
�� 

(
�� 
(
�� 
comment
�� 
!=
�� 
null
�� 
&&
�� 
comment
��  '
.
��' (
Name
��( ,
==
��- /
userName
��0 8
)
��8 9
||
��: <
(
��= >
comment
��> E
!=
��F H
null
��I M
&&
��N P
canModerate
��Q \
)
��\ ]
)
��] ^
{
�� 	
comment
�� 
.
�� 
Body
�� 
=
�� $
DeletedMessageTemplate
�� 1
;
��1 2
await
�� 

unitOfWork
�� 
.
�� 
CommentRepository
�� .
.
��. /
UpdateAsync
��/ :
(
��: ;
comment
��; B
)
��B C
;
��C D
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
}
�� 	
else
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$str
��) ;
)
��; <
;
��< =
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
int
�� !
>
��! "!
CreateNewOrderAsync
��# 6
(
��6 7
IUnitOfWork
��7 B

unitOfWork
��C M
,
��M N
IMapper
��O V

automapper
��W a
,
��a b
Guid
��c g

customerId
��h r
,
��r s
int
��t w
quantity��x �
,��� �
GameModelDto��� �
game��� �
,��� �
int��� �
unitInStock��� �
)��� �
{
�� 
if
�� 

(
�� 
quantity
�� 
>
�� 
unitInStock
�� "
)
��" #
{
�� 	
quantity
�� 
=
�� 
unitInStock
�� "
;
��" #
}
�� 	
if
�� 

(
�� 
game
�� 
.
�� 
Id
�� 
is
�� 
not
�� 
null
�� 
)
��  
{
�� 	
var
�� 
gameInSQLServer
�� 
=
��  !
await
��" '

unitOfWork
��( 2
.
��2 3
GameRepository
��3 A
.
��A B
GetByIdAsync
��B N
(
��N O
(
��O P
Guid
��P T
)
��T U
game
��U Y
.
��Y Z
Id
��Z \
)
��\ ]
;
��] ^
if
�� 
(
�� 
gameInSQLServer
�� 
is
��  "
null
��# '
)
��' (
{
�� 
await
�� $
SqlServerHelperService
�� ,
.
��, -C
5CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync
��- b
(
��b c

unitOfWork
��c m
,
��m n

automapper
��o y
,
��y z
game
��{ 
,�� �
gameInSQLServer��� �
)��� �
;��� �
}
�� 
var
�� 

newOrderId
�� 
=
�� 
Guid
�� !
.
��! "
NewGuid
��" )
(
��) *
)
��* +
;
��+ ,
List
�� 
<
�� 
	OrderGame
�� 
>
�� 

orderGames
�� &
=
��' (
[
��) *
new
��* -
(
��- .
)
��. /
{
��0 1
OrderId
��2 9
=
��: ;

newOrderId
��< F
,
��F G
GameId
��H N
=
��O P
(
��Q R
Guid
��R V
)
��V W
game
��W [
.
��[ \
Id
��\ ^
,
��^ _
Price
��` e
=
��f g
game
��h l
.
��l m
Price
��m r
,
��r s
Discount
��t |
=
��} ~
game�� �
.��� �
Discontinued��� �
,��� �
Quantity��� �
=��� �
quantity��� �
}��� �
]��� �
;��� �
Order
�� 
order
�� 
=
�� 
new
�� 
(
�� 
)
�� 
{
��  !
Id
��" $
=
��% &

newOrderId
��' 1
,
��1 2

CustomerId
��3 =
=
��> ?

customerId
��@ J
,
��J K
	OrderDate
��L U
=
��V W
DateTime
��X `
.
��` a
Now
��a d
,
��d e

OrderGames
��f p
=
��q r

orderGames
��s }
,
��} ~
Status�� �
=��� �
OrderStatus��� �
.��� �
Open��� �
}��� �
;��� �
await
�� 

unitOfWork
�� 
.
�� 
OrderRepository
�� ,
.
��, -
AddAsync
��- 5
(
��5 6
order
��6 ;
)
��; <
;
��< =
await
�� 

unitOfWork
�� 
.
�� !
OrderGameRepository
�� 0
.
��0 1
AddAsync
��1 9
(
��9 :

orderGames
��: D
[
��D E
$num
��E F
]
��F G
)
��G H
;
��H I
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
}
�� 	
return
�� 
quantity
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
Game
�� "
>
��" #&
AddGameToRepositoryAsync
��$ <
(
��< =
IUnitOfWork
��= H

unitOfWork
��I S
,
��S T
GameDtoWrapper
��U c
	gameModel
��d m
,
��m n
Game
��o s
game
��t x
)
��x y
{
�� 
game
�� 
.
�� 
PublisherId
�� 
=
�� 
	gameModel
�� $
.
��$ %
	Publisher
��% .
;
��. /
var
�� 
	addedGame
�� 
=
�� 
await
�� 

unitOfWork
�� (
.
��( )
GameRepository
��) 7
.
��7 8
AddAsync
��8 @
(
��@ A
game
��A E
)
��E F
;
��F G
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
return
�� 
	addedGame
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� /
!AddGamePlatformsToRepositoryAsync
�� ?
(
��? @
IUnitOfWork
��@ K

unitOfWork
��L V
,
��V W
Game
��X \
	addedGame
��] f
,
��f g
List
��h l
<
��l m
Guid
��m q
>
��q r
	platforms
��s |
)
��| }
{
�� 
foreach
�� 
(
�� 
var
�� 

platformId
�� 
in
��  "
	platforms
��# ,
)
��, -
{
�� 	
await
�� 

unitOfWork
�� 
.
�� $
GamePlatformRepository
�� 3
.
��3 4
AddAsync
��4 <
(
��< =
new
��= @
GamePlatform
��A M
(
��M N
)
��N O
{
��P Q
GameId
��R X
=
��Y Z
	addedGame
��[ d
.
��d e
Id
��e g
,
��g h

PlatformId
��i s
=
��t u

platformId��v �
}��� �
)��� �
;��� �
}
�� 	
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� ,
AddGameGenresTorepositoryAsync
�� <
(
��< =
IUnitOfWork
��= H

unitOfWork
��I S
,
��S T
Game
��U Y
	addedGame
��Z c
,
��c d
List
��e i
<
��i j
Guid
��j n
>
��n o
genres
��p v
)
��v w
{
�� 
foreach
�� 
(
�� 
var
�� 
genreId
�� 
in
�� 
genres
��  &
)
��& '
{
�� 	
await
�� 

unitOfWork
�� 
.
�� !
GameGenreRepository
�� 0
.
��0 1
AddAsync
��1 9
(
��9 :
new
��: =

GameGenres
��> H
(
��H I
)
��I J
{
��K L
GameId
��M S
=
��T U
	addedGame
��V _
.
��_ `
Id
��` b
,
��b c
GenreId
��d k
=
��l m
genreId
��n u
}
��v w
)
��w x
;
��x y
}
�� 	
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� )
UpdateGameInrepositoryAsync
�� 9
(
��9 :
IUnitOfWork
��: E

unitOfWork
��F P
,
��P Q
GameDtoWrapper
��R `
	gameModel
��a j
,
��j k
Game
��l p
game
��q u
)
��u v
{
�� 
game
�� 
.
�� 
PublisherId
�� 
=
�� 
	gameModel
�� $
.
��$ %
	Publisher
��% .
;
��. /
game
�� 
.
�� 
ProductCategories
�� 
=
��  
[
��! "
]
��" #
;
��# $
game
�� 
.
�� 
ProductPlatforms
�� 
=
�� 
[
��  !
]
��! "
;
��" #
foreach
�� 
(
�� 
var
�� 
genre
�� 
in
�� 
	gameModel
�� '
.
��' (
Genres
��( .
)
��. /
{
�� 	
game
�� 
.
�� 
ProductCategories
�� "
.
��" #
Add
��# &
(
��& '
new
��' *
(
��* +
)
��+ ,
{
��- .
GameId
��/ 5
=
��6 7
(
��8 9
Guid
��9 =
)
��= >
	gameModel
��> G
.
��G H
Game
��H L
.
��L M
Id
��M O
!
��O P
,
��P Q
GenreId
��R Y
=
��Z [
genre
��\ a
}
��b c
)
��c d
;
��d e
}
�� 	
foreach
�� 
(
�� 
var
�� 
platform
�� 
in
��  
	gameModel
��! *
.
��* +
	Platforms
��+ 4
)
��4 5
{
�� 	
game
�� 
.
�� 
ProductPlatforms
�� !
.
��! "
Add
��" %
(
��% &
new
��& )
(
��) *
)
��* +
{
��, -
GameId
��. 4
=
��5 6
(
��7 8
Guid
��8 <
)
��< =
	gameModel
��= F
.
��F G
Game
��G K
.
��K L
Id
��L N
!
��N O
,
��O P

PlatformId
��Q [
=
��\ ]
platform
��^ f
}
��g h
)
��h i
;
��i j
}
�� 	
await
�� 

unitOfWork
�� 
.
�� 
GameRepository
�� '
.
��' (
UpdateAsync
��( 3
(
��3 4
game
��4 8
)
��8 9
;
��9 :
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 4
&DeleteGamePlatformsFromRepositoryAsync
�� D
(
��D E
IUnitOfWork
��E P

unitOfWork
��Q [
,
��[ \
Guid
��] a
?
��a b
gameId
��c i
)
��i j
{
�� 
if
�� 

(
�� 
gameId
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
var
�� 
gamePlatforms
�� 
=
�� 
await
��  %

unitOfWork
��& 0
.
��0 1$
GamePlatformRepository
��1 G
.
��G H
GetByGameIdAsync
��H X
(
��X Y
(
��Y Z
Guid
��Z ^
)
��^ _
gameId
��_ e
)
��e f
;
��f g
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
gamePlatforms
��! .
)
��. /
{
�� 

unitOfWork
�� 
.
�� $
GamePlatformRepository
�� 1
.
��1 2
Delete
��2 8
(
��8 9
item
��9 =
)
��= >
;
��> ?
}
�� 
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 1
#DeleteGameGenresFromRepositoryAsync
�� A
(
��A B
IUnitOfWork
��B M

unitOfWork
��N X
,
��X Y
Guid
��Z ^
?
��^ _
gameId
��` f
)
��f g
{
�� 
if
�� 

(
�� 
gameId
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
var
�� 

gameGenres
�� 
=
�� 
await
�� "

unitOfWork
��# -
.
��- .!
GameGenreRepository
��. A
.
��A B
GetByGameIdAsync
��B R
(
��R S
(
��S T
Guid
��T X
)
��X Y
gameId
��Y _
)
��_ `
;
��` a
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  

gameGenres
��! +
)
��+ ,
{
�� 

unitOfWork
�� 
.
�� !
GameGenreRepository
�� .
.
��. /
Delete
��/ 5
(
��5 6
item
��6 :
)
��: ;
;
��; <
}
�� 
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 1
#DeleteOrderGamesFromRepositoryAsync
�� A
(
��A B
IUnitOfWork
��B M

unitOfWork
��N X
,
��X Y
Game
��Z ^
?
��^ _
game
��` d
)
��d e
{
�� 
var
�� 

orderGames
�� 
=
�� 
await
�� 

unitOfWork
�� )
.
��) *!
OrderGameRepository
��* =
.
��= >
GetAllAsync
��> I
(
��I J
)
��J K
;
��K L
var
��  
orderGamesToRemove
�� 
=
��  

orderGames
��! +
.
��+ ,
Where
��, 1
(
��1 2
x
��2 3
=>
��4 6
x
��7 8
.
��8 9
GameId
��9 ?
==
��@ B
game
��C G
.
��G H
Id
��H J
)
��J K
;
��K L
foreach
�� 
(
�� 
var
�� 
og
�� 
in
��  
orderGamesToRemove
�� -
)
��- .
{
�� 	

unitOfWork
�� 
.
�� !
OrderGameRepository
�� *
.
��* +
Delete
��+ 1
(
��1 2
og
��2 4
)
��4 5
;
��5 6
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� /
!SoftDeleteGameFromRepositoryAsync
�� ?
(
��? @
IUnitOfWork
��@ K

unitOfWork
��L V
,
��V W
Game
��X \
game
��] a
)
��a b
{
�� 
await
�� 

unitOfWork
�� 
.
�� 
GameRepository
�� '
.
��' (

SoftDelete
��( 2
(
��2 3
game
��3 7
)
��7 8
;
��8 9
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� +
DeleteGameFromRepositoryAsync
�� ;
(
��; <
IUnitOfWork
��< G

unitOfWork
��H R
,
��R S
Game
��T X
game
��Y ]
)
��] ^
{
�� 

unitOfWork
�� 
.
�� 
GameRepository
�� !
.
��! "
Delete
��" (
(
��( )
game
��) -
)
��- .
;
��. /
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
Comment
�� -
ConvertCommentModelDtoToComment
�� :
(
��: ;
CommentModelDto
��; J
comment
��K R
,
��R S
Guid
��T X
gameId
��Y _
)
��_ `
{
�� 
return
�� 
new
�� 
Comment
�� 
(
�� 
)
�� 
{
�� 	
GameId
�� 
=
�� 
gameId
�� 
,
�� 
ParentCommentId
�� 
=
�� 
comment
�� %
.
��% &
ParentId
��& .
,
��. /
Body
�� 
=
�� 
comment
�� 
.
�� 
Comment
�� "
.
��" #
Body
��# '
,
��' (
Name
�� 
=
�� 
comment
�� 
.
�� 
Comment
�� "
.
��" #
Name
��# '
!
��' (
,
��( )
}
�� 	
;
��	 

}
�� 
private
�� 
static
�� 
void
�� "
ComposeQuotedMessage
�� ,
(
��, -
Comment
��- 4
commenttoAdd
��5 A
)
��A B
{
�� 
commenttoAdd
�� 
.
�� 
Body
�� 
=
�� 
commenttoAdd
�� (
.
��( )
Body
��) -
.
��- .
Insert
��. 4
(
��4 5
$num
��5 6
,
��6 7
$str
��8 C
)
��C D
;
��D E
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� )
AddMessageToRepositoryAsync
�� 9
(
��9 :
IUnitOfWork
��: E

unitOfWork
��F P
,
��P Q
Comment
��R Y
commenttoAdd
��Z f
)
��f g
{
�� 
await
�� 

unitOfWork
�� 
.
�� 
CommentRepository
�� *
.
��* +
AddAsync
��+ 3
(
��3 4
commenttoAdd
��4 @
)
��@ A
;
��A B
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
(
�� 
bool
�� #
IsBanned
��$ ,
,
��, -
string
��. 4

BannedTill
��5 ?
)
��? @
>
��@ A!
CheckIfUserIsBanned
��B U
(
��U V
string
��V \
userName
��] e
,
��e f
UserManager
��g r
<
��r s
AppUser
��s z
>
��z {
userManager��| �
)��� �
{
�� 
bool
�� 
isBanned
�� 
=
�� 
false
�� 
;
�� 
string
�� 

bannedTill
�� 
=
�� 
string
�� "
.
��" #
Empty
��# (
;
��( )
var
�� 
u
�� 
=
�� 
await
�� 
userManager
�� !
.
��! "
Users
��" '
.
��' (!
FirstOrDefaultAsync
��( ;
(
��; <
x
��< =
=>
��> @
x
��A B
.
��B C
UserName
��C K
==
��L N
userName
��O W
)
��W X
;
��X Y
if
�� 

(
�� 
u
�� 
is
�� 
not
�� 
null
�� 
&&
�� 
userName
�� %
==
��& (
u
��) *
.
��* +
UserName
��+ 3
&&
��4 6
u
��7 8
.
��8 9

BannedTill
��9 C
>
��D E
DateTime
��F N
.
��N O
Now
��O R
)
��R S
{
�� 	
isBanned
�� 
=
�� 
true
�� 
;
�� 

bannedTill
�� 
=
�� 
u
�� 
.
�� 

BannedTill
�� %
.
��% &
ToString
��& .
(
��. /
)
��/ 0
;
��0 1
return
�� 
(
�� 
isBanned
�� 
,
�� 

bannedTill
�� (
)
��( )
;
��) *
}
�� 	
return
�� 
(
�� 
false
�� 
,
�� 

bannedTill
�� !
)
��! "
;
��" #
}
�� 
private
�� 
static
�� 
void
�� 1
#SetTotalNumberOfPagesAfterFiltering
�� ;
(
��; <
GameFiltersDto
��< J
gameFilters
��K V
,
��V W
FilteredGamesDto
��X h
filteredGameDtos
��i y
)
��y z
{
�� 
filteredGameDtos
�� 
.
�� 

TotalPages
�� #
=
��$ %
gameFilters
��& 1
.
��1 2*
NumberOfPagesAfterFiltration
��2 N
;
��N O
}
�� 
private
�� 
static
�� 
void
�� >
0CheckIfCurrentPageDoesntExceedTotalNumberOfPages
�� H
(
��H I
GameFiltersDto
��I W
gameFilters
��X c
,
��c d
FilteredGamesDto
��e u
filteredGameDtos��v �
)��� �
{
�� 
if
�� 

(
�� 
gameFilters
�� 
.
�� 
Page
�� 
<=
�� 
gameFilters
��  +
.
��+ ,*
NumberOfPagesAfterFiltration
��, H
)
��H I
{
�� 	
filteredGameDtos
�� 
.
�� 
CurrentPage
�� (
=
��) *
gameFilters
��+ 6
.
��6 7
Page
��7 ;
;
��; <
}
�� 	
else
�� 
{
�� 	
filteredGameDtos
�� 
.
�� 
CurrentPage
�� (
=
��) *
gameFilters
��+ 6
.
��6 7*
NumberOfPagesAfterFiltration
��7 S
;
��S T
}
�� 	
}
�� 
}�� ��
dD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\GenreService.cs
	namespace 	
	Gamestore
 
. 
Services 
. 
Services %
;% &
public 
class 
GenreService 
( 
IUnitOfWork %

unitOfWork& 0
,0 1
IMongoUnitOfWork2 B
mongoUnitOfWorkC R
,R S
IMapperT [

automapper\ f
,f g
ILoggerh o
<o p
GenreServicep |
>| }
logger	~ �
)
� �
:
� �
IGenreService
� �
{ 
private 
readonly '
GenreDtoWrapperAddValidator 0(
_genreDtoWrapperAddValidator1 M
=N O
newP S
(S T

unitOfWorkT ^
)^ _
;_ `
private 
readonly *
GenreDtoWrapperUpdateValidator 3+
_genreDtoWrapperUpdateValidator4 S
=T U
newV Y
(Y Z

unitOfWorkZ d
)d e
;e f
public 

async 
Task 
DeleteGenreAsync &
(& '
Guid' +
genreId, 3
)3 4
{ 
logger 
. 
LogInformation 
( 
$str 8
,8 9
genreId: A
)A B
;B C
var 
childGenres 
= 
await 

unitOfWork  *
.* +
GenreRepository+ :
.: ;'
GetGenresByParentGenreAsync; V
(V W
genreIdW ^
)^ _
;_ `
if 

( 
childGenres 
. 
Count 
!=  
$num! "
)" #
{ 	
throw 
new 
GamestoreException (
(( )
$") +
$str+ [
{[ \
genreId\ c
}c d
"d e
)e f
;f g
} 	
var 
genre 
= 
await 

unitOfWork $
.$ %
GenreRepository% 4
.4 5
GetByIdAsync5 A
(A B
genreIdB I
)I J
;J K
if   

(   
genre   
!=   
null   
)   
{!! 	

unitOfWork"" 
."" 
GenreRepository"" &
.""& '
Delete""' -
(""- .
genre"". 3
)""3 4
;""4 5
await## 

unitOfWork## 
.## 
	SaveAsync## &
(##& '
)##' (
;##( )
}$$ 	
else%% 
{&& 	
throw'' 
new'' 
GamestoreException'' (
(''( )
$"'') +
$str''+ I
{''I J
genreId''J Q
}''Q R
"''R S
)''S T
;''T U
}(( 	
})) 
public++ 

async++ 
Task++ 
<++ 
IEnumerable++ !
<++! "
GenreModelDto++" /
>++/ 0
>++0 1
GetAllGenresAsync++2 C
(++C D
)++D E
{,, 
logger-- 
.-- 
LogInformation-- 
(-- 
$str-- 2
)--2 3
;--3 4
var// 
genreModels// 
=// 
await// "
GetGenresFromSQLServer//  6
(//6 7

unitOfWork//7 A
,//A B

automapper//C M
)//M N
;//N O
genreModels00 
.00 
AddRange00 
(00 
(00 
await00 #$
GetCategoriesFromMongoDB00$ <
(00< =
mongoUnitOfWork00= L
,00L M

automapper00N X
)00X Y
)00Y Z
.00Z [
Except00[ a
(00a b
genreModels00b m
)00m n
)00n o
;00o p
return22 
genreModels22 
.22 
AsEnumerable22 '
(22' (
)22( )
;22) *
}33 
public55 

async55 
Task55 
<55 
IEnumerable55 !
<55! "
GameModelDto55" .
>55. /
>55/ 0 
GetGamesByGenreAsync551 E
(55E F
Guid55F J
genreId55K R
)55R S
{66 
logger77 
.77 
LogInformation77 
(77 
$str77 A
,77A B
genreId77C J
)77J K
;77K L
var99 
games99 
=99 
await99 *
GetGamesByGenreIdFromSQLServer99 8
(998 9

unitOfWork999 C
,99C D

automapper99E O
,99O P
genreId99Q X
)99X Y
;99Y Z
games:: 
.:: 
AddRange:: 
(:: 
await:: (
GetGamesByGenreIdFromMongoDB:: 9
(::9 :
mongoUnitOfWork::: I
,::I J

automapper::K U
,::U V
genreId::W ^
,::^ _
games::` e
)::e f
)::f g
;::g h
return<< 
games<< 
;<< 
}== 
public?? 

async?? 
Task?? 
<?? 
GenreModelDto?? #
>??# $
GetGenreByIdAsync??% 6
(??6 7
Guid??7 ;
genreId??< C
)??C D
{@@ 
loggerAA 
.AA 
LogInformationAA 
(AA 
$strAA >
,AA> ?
genreIdAA@ G
)AAG H
;AAH I
varCC 
genreCC 
=CC 
awaitCC %
GetGenreFromSQLServerByIdCC 3
(CC3 4

unitOfWorkCC4 >
,CC> ?
genreIdCC@ G
)CCG H
;CCH I
genreDD 
??=DD 
awaitDD 
GetGenreFromMongoDBDD +
(DD+ ,
mongoUnitOfWorkDD, ;
,DD; <

automapperDD= G
,DDG H
genreIdDDI P
)DDP Q
;DDQ R
returnFF 
genreFF 
==FF 
nullFF 
?FF 
throwFF $
newFF% (
GamestoreExceptionFF) ;
(FF; <
$"FF< >
$strFF> \
{FF\ ]
genreIdFF] d
}FFd e
"FFe f
)FFf g
:FFh i

automapperFFj t
.FFt u
MapFFu x
<FFx y
GenreModelDto	FFy �
>
FF� �
(
FF� �
genre
FF� �
)
FF� �
;
FF� �
}GG 
publicII 

asyncII 
TaskII 
<II 
IEnumerableII !
<II! "
GenreModelDtoII" /
>II/ 0
>II0 1'
GetGenresByParentGenreAsyncII2 M
(IIM N
GuidIIN R
genreIdIIS Z
)IIZ [
{JJ 
loggerKK 
.KK 
LogInformationKK 
(KK 
$strKK L
,KKL M
genreIdKKN U
)KKU V
;KKV W
varLL 
genresLL 
=LL 
awaitLL 

unitOfWorkLL %
.LL% &
GenreRepositoryLL& 5
.LL5 6'
GetGenresByParentGenreAsyncLL6 Q
(LLQ R
genreIdLLR Y
)LLY Z
;LLZ [
ListMM 
<MM 
GenreModelDtoMM 
>MM 
genreModelsMM '
=MM( )

automapperMM* 4
.MM4 5
MapMM5 8
<MM8 9
ListMM9 =
<MM= >
GenreModelDtoMM> K
>MMK L
>MML M
(MMM N
genresMMN T
)MMT U
;MMU V
returnOO 
genreModelsOO 
.OO 
AsEnumerableOO '
(OO' (
)OO( )
;OO) *
}PP 
publicRR 

asyncRR 
TaskRR 
AddGenreAsyncRR #
(RR# $
GenreDtoWrapperRR$ 3

genreModelRR4 >
)RR> ?
{SS 
loggerTT 
.TT 
LogInformationTT 
(TT 
$strTT :
,TT: ;

genreModelTT< F
)TTF G
;TTG H
awaitVV (
_genreDtoWrapperAddValidatorVV *
.VV* +"
ValidateGenreForAddingVV+ A
(VVA B

genreModelVVB L
)VVL M
;VVM N
varXX 
genreXX 
=XX 

automapperXX 
.XX 
MapXX "
<XX" #
GenreXX# (
>XX( )
(XX) *

genreModelXX* 4
.XX4 5
GenreXX5 :
)XX: ;
;XX; <
awaitZZ 

unitOfWorkZZ 
.ZZ 
GenreRepositoryZZ (
.ZZ( )
AddAsyncZZ) 1
(ZZ1 2
genreZZ2 7
)ZZ7 8
;ZZ8 9
await\\ 

unitOfWork\\ 
.\\ 
	SaveAsync\\ "
(\\" #
)\\# $
;\\$ %
}]] 
public__ 

async__ 
Task__ 
UpdateGenreAsync__ &
(__& '
GenreDtoWrapper__' 6

genreModel__7 A
)__A B
{`` 
loggeraa 
.aa 
LogInformationaa 
(aa 
$straa <
,aa< =

genreModelaa> H
)aaH I
;aaI J
awaitcc +
_genreDtoWrapperUpdateValidatorcc -
.cc- .$
ValidateGenreForUpdatingcc. F
(ccF G

genreModelccG Q
)ccQ R
;ccR S
varee 
genreee 
=ee 

automapperee 
.ee 
Mapee "
<ee" #
Genreee# (
>ee( )
(ee) *

genreModelee* 4
.ee4 5
Genreee5 :
)ee: ;
;ee; <
awaitgg 

unitOfWorkgg 
.gg 
GenreRepositorygg (
.gg( )
UpdateAsyncgg) 4
(gg4 5
genregg5 :
)gg: ;
;gg; <
awaitii 

unitOfWorkii 
.ii 
	SaveAsyncii "
(ii" #
)ii# $
;ii$ %
}jj 
privatell 
staticll 
intll 1
%ConvertFirstEightCharactersOfGuidToIdll <
(ll< =
Guidll= A
genreIdllB I
)llI J
{mm 
returnnn 
intnn 
.nn 
Parsenn 
(nn 
genreIdnn  
.nn  !
ToStringnn! )
(nn) *
)nn* +
[nn+ ,
..nn, .
$numnn. /
]nn/ 0
)nn0 1
;nn1 2
}oo 
privateqq 
staticqq 
asyncqq 
Taskqq 
<qq 
Genreqq #
?qq# $
>qq$ %%
GetGenreFromSQLServerByIdqq& ?
(qq? @
IUnitOfWorkqq@ K

unitOfWorkqqL V
,qqV W
GuidqqX \
genreIdqq] d
)qqd e
{rr 
returnss 
awaitss 

unitOfWorkss 
.ss  
GenreRepositoryss  /
.ss/ 0
GetByIdAsyncss0 <
(ss< =
genreIdss= D
)ssD E
;ssE F
}tt 
privatevv 
staticvv 
asyncvv 
Taskvv 
<vv 
Genrevv #
?vv# $
>vv$ %
GetGenreFromMongoDBvv& 9
(vv9 :
IMongoUnitOfWorkvv: J
mongoUnitOfWorkvvK Z
,vvZ [
IMappervv\ c

automappervvd n
,vvn o
Guidvvp t
genreIdvvu |
)vv| }
{ww 
intxx 
idxx 
=xx 1
%ConvertFirstEightCharactersOfGuidToIdxx 6
(xx6 7
genreIdxx7 >
)xx> ?
;xx? @
varyy 
categoryyy 
=yy 
awaityy 
mongoUnitOfWorkyy ,
.yy, -
CategoryRepositoryyy- ?
.yy? @
GetByIdyy@ G
(yyG H
idyyH J
)yyJ K
;yyK L
varzz 
genrezz 
=zz 

automapperzz 
.zz 
Mapzz "
<zz" #
Genrezz# (
>zz( )
(zz) *
categoryzz* 2
)zz2 3
;zz3 4
return|| 
genre|| 
;|| 
}}} 
private 
static 
async 
Task 
< 
List "
<" #
GameModelDto# /
>/ 0
>0 1*
GetGamesByGenreIdFromSQLServer2 P
(P Q
IUnitOfWorkQ \

unitOfWork] g
,g h
IMapperi p

automapperq {
,{ |
Guid	} �
genreId
� �
)
� �
{
�� 
List
�� 
<
�� 
GameModelDto
�� 
>
�� 

gameModels
�� %
=
��& '
[
��( )
]
��) *
;
��* +
var
�� 
games
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
GenreRepository
��% 4
.
��4 5"
GetGamesByGenreAsync
��5 I
(
��I J
genreId
��J Q
)
��Q R
;
��R S
if
�� 

(
�� 
games
�� 
is
�� 
not
�� 
null
�� 
)
�� 
{
�� 	

gameModels
�� 
=
�� 

automapper
�� #
.
��# $
Map
��$ '
<
��' (
List
��( ,
<
��, -
GameModelDto
��- 9
>
��9 :
>
��: ;
(
��; <
games
��< A
)
��A B
;
��B C
}
�� 	
return
�� 

gameModels
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
List
�� "
<
��" #
GameModelDto
��# /
>
��/ 0
>
��0 1*
GetGamesByGenreIdFromMongoDB
��2 N
(
��N O
IMongoUnitOfWork
��O _
mongoUnitOfWork
��` o
,
��o p
IMapper
��q x

automapper��y �
,��� �
Guid��� �
genreId��� �
,��� �
List��� �
<��� �
GameModelDto��� �
>��� �'
gamesFromPreviousSource��� �
)��� �
{
�� 
List
�� 
<
�� 
GameModelDto
�� 
>
�� 
games
��  
=
��! "
[
��# $
]
��$ %
;
��% &
int
�� 
id
�� 
=
�� 
GuidHelpers
�� 
.
�� 
	GuidToInt
�� &
(
��& '
genreId
��' .
)
��. /
;
��/ 0
var
�� 
category
�� 
=
�� 
await
�� 
mongoUnitOfWork
�� ,
.
��, - 
CategoryRepository
��- ?
.
��? @
GetById
��@ G
(
��G H
id
��H J
)
��J K
;
��K L
if
�� 

(
�� 
category
�� 
is
�� 
not
�� 
null
��  
)
��  !
{
�� 	
var
�� 
products
�� 
=
�� 
await
��  
mongoUnitOfWork
��! 0
.
��0 1
ProductRepository
��1 B
.
��B C"
GetByCategoryIdAsync
��C W
(
��W X
category
��X `
.
��` a

CategoryId
��a k
)
��k l
;
��l m
if
�� 
(
�� 
products
�� 
is
�� 
not
�� 
null
��  $
)
��$ %
{
�� 
games
�� 
=
�� 

automapper
�� "
.
��" #
Map
��# &
<
��& '
List
��' +
<
��+ ,
GameModelDto
��, 8
>
��8 9
>
��9 :
(
��: ;
products
��; C
)
��C D
;
��D E
}
�� 
}
�� 	
return
�� 
games
�� 
.
�� 
Except
�� 
(
�� %
gamesFromPreviousSource
�� 3
)
��3 4
.
��4 5
ToList
��5 ;
(
��; <
)
��< =
;
��= >
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
List
�� "
<
��" #
GenreModelDto
��# 0
>
��0 1
>
��1 2$
GetGenresFromSQLServer
��3 I
(
��I J
IUnitOfWork
��J U

unitOfWork
��V `
,
��` a
IMapper
��b i

automapper
��j t
)
��t u
{
�� 
var
�� 
genres
�� 
=
�� 
await
�� 

unitOfWork
�� %
.
��% &
GenreRepository
��& 5
.
��5 6
GetAllAsync
��6 A
(
��A B
)
��B C
;
��C D
var
�� 
genreModels
�� 
=
�� 

automapper
�� $
.
��$ %
Map
��% (
<
��( )
List
��) -
<
��- .
GenreModelDto
��. ;
>
��; <
>
��< =
(
��= >
genres
��> D
)
��D E
;
��E F
return
�� 
genreModels
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
List
�� "
<
��" #
GenreModelDto
��# 0
>
��0 1
>
��1 2&
GetCategoriesFromMongoDB
��3 K
(
��K L
IMongoUnitOfWork
��L \
mongoUnitOfWork
��] l
,
��l m
IMapper
��n u

automapper��v �
)��� �
{
�� 
var
�� 

categories
�� 
=
�� 
await
�� 
mongoUnitOfWork
�� .
.
��. / 
CategoryRepository
��/ A
.
��A B
GetAllAsync
��B M
(
��M N
)
��N O
;
��O P
var
�� 
genres
�� 
=
�� 

automapper
�� 
.
��  
Map
��  #
<
��# $
List
��$ (
<
��( )
GenreModelDto
��) 6
>
��6 7
>
��7 8
(
��8 9

categories
��9 C
)
��C D
;
��D E
return
�� 
genres
�� 
;
�� 
}
�� 
}�� �`
lD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\MongoDbHelperService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
internal 
static	 
class  
MongoDbHelperService *
{ 
internal 
static 
async 
Task *
FilterProductsFromMongoDBAsync =
(= >
IUnitOfWork> I

unitOfWorkJ T
,T U
IMongoUnitOfWorkV f
mongoUnitOfWorkg v
,v w
IMapperx 

automapper
� �
,
� �
GameFiltersDto
� �
gameFilters
� �
,
� �
FilteredGamesDto
� �
filteredGameDtos
� �
,
� �,
IGameProcessingPipelineService
� �+
gameProcessingPipelineService
� �
)
� �
{ 
var 
products 
= 
await A
5GetProductsFromMongoDBThatDoesntExistInSQLServerAsync R
(R S

unitOfWorkS ]
,] ^
mongoUnitOfWork_ n
,n o

automapperp z
)z {
;{ |
var 
filterdProducts 
= 
( 
await $)
gameProcessingPipelineService% B
.B C
ProcessGamesAsyncC T
(T U

unitOfWorkU _
,_ `
mongoUnitOfWorka p
,p q
gameFiltersr }
,} ~
products	 �
.
� �
AsQueryable
� �
(
� �
)
� �
)
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
if 

( 
filterdProducts 
. 
Count !
!=" $
$num% &
)& '
{ 	
filteredGameDtos 
. 
Games "
." #
AddRange# +
(+ ,

automapper, 6
.6 7
Map7 :
<: ;
List; ?
<? @
GameModelDto@ L
>L M
>M N
(N O
filterdProductsO ^
)^ _
)_ `
;` a
} 	
} 
internal 
static 
async 
Task 
< 
List #
<# $
GenreModelDto$ 1
>1 2
>2 3.
"GetGenresFromMongoDBByGameKeyAsync4 V
(V W
IMongoUnitOfWorkW g
mongoUnitOfWorkh w
,w x
IMapper	y �

automapper
� �
,
� �
string
� �
gameKey
� �
)
� �
{ 
var 
product 
= 
await 
mongoUnitOfWork +
.+ ,
ProductRepository, =
.= >
GetByNameAsync> L
(L M
gameKeyM T
)T U
;U V
var 
category 
= 
await 
mongoUnitOfWork ,
., -
CategoryRepository- ?
.? @
GetById@ G
(G H
productH O
.O P

CategoryIDP Z
)Z [
;[ \
return 
[ 

automapper 
. 
Map 
< 
GenreModelDto ,
>, -
(- .
category. 6
)6 7
]7 8
;8 9
}   
internal"" 
static"" 
async"" 
Task"" 
<"" 
PublisherModelDto"" 0
>""0 11
%GetPublisherFromMongoDBByGameKeyAsync""2 W
(""W X
IMongoUnitOfWork""X h
mongoUnitOfWork""i x
,""x y
IMapper	""z �

automapper
""� �
,
""� �
string
""� �
gameKey
""� �
)
""� �
{## 
var$$ 
product$$ 
=$$ 
await$$ 
mongoUnitOfWork$$ +
.$$+ ,
ProductRepository$$, =
.$$= >
GetByNameAsync$$> L
($$L M
gameKey$$M T
)$$T U
;$$U V
var%% 
supplier%% 
=%% 
await%% 
mongoUnitOfWork%% ,
.%%, -
SupplierRepository%%- ?
.%%? @
GetByIdAsync%%@ L
(%%L M
product%%M T
.%%T U

SupplierID%%U _
)%%_ `
;%%` a
return'' 

automapper'' 
.'' 
Map'' 
<'' 
PublisherModelDto'' /
>''/ 0
(''0 1
supplier''1 9
)''9 :
;'': ;
}(( 
internal** 
static** 
async** 
Task** 
<** 
GameModelDto** +
>**+ ,3
'GetGameWithDetailsFromMongoDBByKeyAsync**- T
(**T U
IMongoUnitOfWork**U e
mongoUnitOfWork**f u
,**u v
IMapper**w ~

automapper	** �
,
**� �
string
**� �
key
**� �
)
**� �
{++ 
var,, 
product,, 
=,, 
await,, 
mongoUnitOfWork,, +
.,,+ ,
ProductRepository,,, =
.,,= >
GetByNameAsync,,> L
(,,L M
key,,M P
),,P Q
;,,Q R
var-- 
gameFromProduct-- 
=-- 

automapper-- (
.--( )
Map--) ,
<--, -
GameModelDto--- 9
>--9 :
(--: ;
product--; B
)--B C
;--C D
await.. -
!SetGameDetailsForMongoDBGameAsync.. /
(../ 0
mongoUnitOfWork..0 ?
,..? @
gameFromProduct..A P
)..P Q
;..Q R
return00 
gameFromProduct00 
;00 
}11 
internal33 
static33 
async33 
Task33 
<33 
GameModelDto33 +
>33+ ,2
&GetGameWithDetailsFromMongoDBByIdAsync33- S
(33S T
IMongoUnitOfWork33T d
mongoUnitOfWork33e t
,33t u
IMapper33v }

automapper	33~ �
,
33� �
int
33� �
id
33� �
)
33� �
{44 
var55 
product55 
=55 
await55 
mongoUnitOfWork55 +
.55+ ,
ProductRepository55, =
.55= >
GetByIdAsync55> J
(55J K
id55K M
)55M N
;55N O
var66 
gameFromProduct66 
=66 

automapper66 (
.66( )
Map66) ,
<66, -
GameModelDto66- 9
>669 :
(66: ;
product66; B
)66B C
;66C D
await77 -
!SetGameDetailsForMongoDBGameAsync77 /
(77/ 0
mongoUnitOfWork770 ?
,77? @
gameFromProduct77A P
)77P Q
;77Q R
return99 
gameFromProduct99 
;99 
}:: 
internal<< 
static<< 
async<< 
Task<< 
<<< 
Game<< #
?<<# $
><<$ %'
GetGameFromMongoDBByIdAsync<<& A
(<<A B
IMongoUnitOfWork<<B R
mongoUnitOfWork<<S b
,<<b c
IMapper<<d k

automapper<<l v
,<<v w
Guid<<x |
gameId	<<} �
)
<<� �
{== 
int>> 
id>> 
=>> 
GuidHelpers>> 
.>> 
	GuidToInt>> &
(>>& '
gameId>>' -
)>>- .
;>>. /
var?? 
product?? 
=?? 
await?? 
mongoUnitOfWork?? +
.??+ ,
ProductRepository??, =
.??= >
GetByIdAsync??> J
(??J K
id??K M
)??M N
;??N O
var@@ 
game@@ 
=@@ 

automapper@@ 
.@@ 
Map@@ !
<@@! "
Game@@" &
>@@& '
(@@' (
product@@( /
)@@/ 0
;@@0 1
returnAA 
gameAA 
;AA 
}BB 
privateDD 
staticDD 
asyncDD 
TaskDD -
!SetGameDetailsForMongoDBGameAsyncDD ?
(DD? @
IMongoUnitOfWorkDD@ P
mongoUnitOfWorkDDQ `
,DD` a
GameModelDtoDDb n
gameFromProductDDo ~
)DD~ 
{EE 
ifFF 

(FF 
gameFromProductFF 
isFF 
notFF "
nullFF# '
&&FF( *
gameFromProductFF+ :
.FF: ;
	PublisherFF; D
.FFD E
IdFFE G
isFFH J
notFFK N
nullFFO S
&&FFT V
gameFromProductFFW f
.FFf g
	PublisherFFg p
.FFp q
IdFFq s
!=FFt v
GuidFFw {
.FF{ |
Empty	FF| �
)
FF� �
{GG 	
gameFromProductHH 
.HH 
	PublisherHH %
.HH% &
CompanyNameHH& 1
=HH2 3
(HH4 5
awaitHH5 :
mongoUnitOfWorkHH; J
.HHJ K
SupplierRepositoryHHK ]
.HH] ^
GetByIdAsyncHH^ j
(HHj k
GuidHelpersHHk v
.HHv w
	GuidToInt	HHw �
(
HH� �
(
HH� �
Guid
HH� �
)
HH� �
gameFromProduct
HH� �
.
HH� �
	Publisher
HH� �
.
HH� �
Id
HH� �
!
HH� �
)
HH� �
)
HH� �
)
HH� �
.
HH� �
CompanyName
HH� �
;
HH� �
}II 	
ifKK 

(KK 
gameFromProductKK 
isKK 
notKK "
nullKK# '
&&KK( *
gameFromProductKK+ :
.KK: ;
GenresKK; A
[KKA B
$numKKB C
]KKC D
isKKE G
notKKH K
nullKKL P
&&KKQ S
gameFromProductKKT c
.KKc d
GenresKKd j
[KKj k
$numKKk l
]KKl m
.KKm n
IdKKn p
!=KKq s
GuidKKt x
.KKx y
EmptyKKy ~
)KK~ 
{LL 	
gameFromProductMM 
.MM 
GenresMM "
[MM" #
$numMM# $
]MM$ %
.MM% &
NameMM& *
=MM+ ,
(MM- .
awaitMM. 3
mongoUnitOfWorkMM4 C
.MMC D
CategoryRepositoryMMD V
.MMV W
GetByIdMMW ^
(MM^ _
GuidHelpersMM_ j
.MMj k
	GuidToIntMMk t
(MMt u
(MMu v
GuidMMv z
)MMz {
gameFromProduct	MM{ �
.
MM� �
Genres
MM� �
[
MM� �
$num
MM� �
]
MM� �
.
MM� �
Id
MM� �
!
MM� �
)
MM� �
)
MM� �
)
MM� �
.
MM� �
CategoryName
MM� �
;
MM� �
}NN 	
}OO 
privateQQ 
staticQQ 
asyncQQ 
TaskQQ 
<QQ 
ListQQ "
<QQ" #
GameQQ# '
>QQ' (
>QQ( )A
5GetProductsFromMongoDBThatDoesntExistInSQLServerAsyncQQ* _
(QQ_ `
IUnitOfWorkQQ` k

unitOfWorkQQl v
,QQv w
IMongoUnitOfWork	QQx �
mongoUnitOfWork
QQ� �
,
QQ� �
IMapper
QQ� �

automapper
QQ� �
)
QQ� �
{RR 
varSS 
productsFromMongoDBSS 
=SS  !

automapperSS" ,
.SS, -
MapSS- 0
<SS0 1
ListSS1 5
<SS5 6
GameSS6 :
>SS: ;
>SS; <
(SS< =
awaitSS= B
mongoUnitOfWorkSSC R
.SSR S
ProductRepositorySSS d
.SSd e
GetAllAsyncSSe p
(SSp q
)SSq r
)SSr s
;SSs t
returnTT 
productsFromMongoDBTT "
.TT" #
ExceptTT# )
(TT) *
awaitTT* /

unitOfWorkTT0 :
.TT: ;
GameRepositoryTT; I
.TTI J
GetAllAsyncTTJ U
(TTU V
)TTV W
)TTW X
.TTX Y
ToListTTY _
(TT_ `
)TT` a
;TTa b
}UU 
}VV �
dD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\OrderService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
public 
class 
OrderService 
( 
IUnitOfWork %

unitOfWork& 0
,0 1
IMongoUnitOfWork2 B
mongoUnitOfWorkC R
,R S
IMapperT [

automapper\ f
,f g
ILoggerh o
<o p
OrderServicep |
>| }
logger	~ �
,
� �
IOptions
� �
<
� �)
PaymentServiceConfiguration
� �
>
� �)
paymentServiceConfiguration
� �
)
� �
:
� �
IOrderService
� �
{ 
private 
readonly  
VisaPaymentValidator )!
_visaPaymentValidator* ?
=@ A
newB E
(E F
)F G
;G H
public 

async 
Task 
< 
OrderModelDto #
># $
GetOrderByIdAsync% 6
(6 7
Guid7 ;
orderId< C
)C D
{ 
logger   
.   
LogInformation   
(   
$str   >
,  > ?
orderId  @ G
)  G H
;  H I
var!! 
order!! 
=!! 
await!! %
GetOrderFromSQLServerById!! 3
(!!3 4

unitOfWork!!4 >
,!!> ?
orderId!!@ G
)!!G H
;!!H I
order## 
??=## 
await## "
GetOrderFromMongDBById## .
(##. /
mongoUnitOfWork##/ >
,##> ?

automapper##@ J
,##J K
orderId##L S
)##S T
;##T U
return%% 
order%% 
==%% 
null%% 
?%% 
throw%% $
new%%% (
GamestoreException%%) ;
(%%; <
$"%%< >
$str%%> \
{%%\ ]
orderId%%] d
}%%d e
"%%e f
)%%f g
:%%h i

automapper%%j t
.%%t u
Map%%u x
<%%x y
OrderModelDto	%%y �
>
%%� �
(
%%� �
order
%%� �
)
%%� �
;
%%� �
}&& 
public(( 

async(( 
Task(( 
<(( 
List(( 
<(( 
OrderModelDto(( (
>((( )
>(() *
GetAllOrdersAsync((+ <
(((< =
)((= >
{)) 
logger** 
.** 
LogInformation** 
(** 
$str** 2
)**2 3
;**3 4
var++ 
orders++ 
=++ 
await++ 

unitOfWork++ %
.++% &
OrderRepository++& 5
.++5 6
GetAllAsync++6 A
(++A B
)++B C
;++C D
List-- 
<-- 
OrderModelDto-- 
>-- 
orderModels-- '
=--( )
[--* +
]--+ ,
;--, -'
AddSQLServerOrdersToDtoList.. #
(..# $

automapper..$ .
,... /
orders..0 6
,..6 7
orderModels..8 C
)..C D
;..D E
return00 
orderModels00 
;00 
}11 
public33 

async33 
Task33 
<33 
List33 
<33 
OrderModelDto33 (
>33( )
>33) *!
GetOrdersHistoryAsync33+ @
(33@ A
string33A G
?33G H
	startDate33I R
,33R S
string33T Z
?33Z [
endDate33\ c
)33c d
{44 
logger55 
.55 
LogInformation55 
(55 
$str55 6
)556 7
;557 8
List66 
<66 
OrderModelDto66 
>66 
orderModels66 '
=66( )
[66* +
]66+ ,
;66, -*
ParseDateRangeToDateTimeFormat88 &
(88& '
ref88' *
	startDate88+ 4
,884 5
ref886 9
endDate88: A
,88A B
out88C F
var88G J
startD88K Q
,88Q R
out88S V
var88W Z
endD88[ _
)88_ `
;88` a
var:: 
orders:: 
=:: 
await:: 

unitOfWork:: %
.::% &
OrderRepository::& 5
.::5 6%
GetOrdersByDateRangeAsync::6 O
(::O P
startD::P V
,::V W
endD::X \
)::\ ]
;::] ^'
AddSQLServerOrdersToDtoList;; #
(;;# $

automapper;;$ .
,;;. /
orders;;0 6
,;;6 7
orderModels;;8 C
);;C D
;;;D E
var== 
ordersFromMongo== 
=== 
await== #
mongoUnitOfWork==$ 3
.==3 4
OrderRepository==4 C
.==C D
GetAllAsync==D O
(==O P
)==P Q
;==Q R#
AddMongoOrdersToDtoList>> 
(>>  

automapper>>  *
,>>* +
ordersFromMongo>>, ;
,>>; <
startD>>= C
,>>C D
endD>>E I
,>>I J
orderModels>>K V
)>>V W
;>>W X
return@@ 
orderModels@@ 
;@@ 
}AA 
publicCC 

asyncCC 
TaskCC  
DeleteOrderByIdAsyncCC *
(CC* +
GuidCC+ /
orderIdCC0 7
)CC7 8
{DD 
loggerEE 
.EE 
LogInformationEE 
(EE 
$strEE ?
,EE? @
orderIdEEA H
)EEH I
;EEI J
varGG 
orderGG 
=GG 
awaitGG 

unitOfWorkGG $
.GG$ %
OrderRepositoryGG% 4
.GG4 5
GetByIdAsyncGG5 A
(GGA B
orderIdGGB I
)GGI J
;GGJ K
ifHH 

(HH 
orderHH 
!=HH 
nullHH 
)HH 
{II 	
awaitJJ 
DeleteOrderJJ 
(JJ 

unitOfWorkJJ (
,JJ( )
orderJJ* /
)JJ/ 0
;JJ0 1
}KK 	
elseLL 
{MM 	
throwNN 
newNN 
GamestoreExceptionNN (
(NN( )
$"NN) +
$strNN+ I
{NNI J
orderIdNNJ Q
}NNQ R
"NNR S
)NNS T
;NNT U
}OO 	
}PP 
publicRR 

asyncRR 
TaskRR 
<RR 
ListRR 
<RR 
OrderDetailsDtoRR *
>RR* +
>RR+ ,)
GetOrderDetailsByOrderIdAsyncRR- J
(RRJ K
GuidRRK O
orderIdRRP W
)RRW X
{SS 
loggerTT 
.TT 
LogInformationTT 
(TT 
$strTT F
,TTF G
orderIdTTH O
)TTO P
;TTP Q
ListUU 
<UU 
OrderDetailsDtoUU 
>UU 
orederDetailsUU +
=UU, -
[UU. /
]UU/ 0
;UU0 1
varWW 
orderWW 
=WW 
awaitWW 

unitOfWorkWW $
.WW$ %
OrderRepositoryWW% 4
.WW4 5#
GetWithDetailsByIdAsyncWW5 L
(WWL M
orderIdWWM T
)WWT U
;WWU V
ifXX 

(XX 
orderXX 
isXX 
notXX 
nullXX 
)XX 
{YY 	-
!AddSQLServerOrderDetailsToDtoListZZ -
(ZZ- .

automapperZZ. 8
,ZZ8 9
orderZZ: ?
,ZZ? @
orederDetailsZZA N
)ZZN O
;ZZO P
return[[ 
orederDetails[[  
;[[  !
}\\ 	
int^^ 
id^^ 
=^^ 
GuidHelpers^^ 
.^^ 
	GuidToInt^^ &
(^^& '
orderId^^' .
)^^. /
;^^/ 0
order__ 
=__ 

automapper__ 
.__ 
Map__ 
<__ 
Order__ $
>__$ %
(__% &
await__& +
mongoUnitOfWork__, ;
.__; <
OrderRepository__< K
.__K L
GetByIdAsync__L X
(__X Y
id__Y [
)__[ \
)__\ ]
;__] ^
if`` 

(`` 
order`` 
is`` 
not`` 
null`` 
)`` 
{aa 	
awaitbb +
AddMongoDBOrderDetailsToDtoListbb 1
(bb1 2
mongoUnitOfWorkbb2 A
,bbA B

automapperbbC M
,bbM N
orderbbO T
,bbT U
orederDetailsbbV c
)bbc d
;bbd e
returncc 
orederDetailscc  
;cc  !
}dd 	
returnff 
orederDetailsff 
;ff 
}gg 
publicii 

asyncii 
Taskii 
<ii 
Listii 
<ii 
OrderDetailsDtoii *
>ii* +
>ii+ ,$
GetCartByCustomerIdAsyncii- E
(iiE F
GuidiiF J

customerIdiiK U
)iiU V
{jj 
loggerkk 
.kk 
LogInformationkk 
(kk 
$strkk ,
)kk, -
;kk- .
varll 
orderll 
=ll 
awaitll 

unitOfWorkll $
.ll$ %
OrderRepositoryll% 4
.ll4 5 
GetByCustomerIdAsyncll5 I
(llI J

customerIdllJ T
)llT U
;llU V
Listnn 
<nn 
OrderDetailsDtonn 
>nn 
orederDetailsnn +
=nn, -
[nn. /
]nn/ 0
;nn0 1
ifoo 

(oo 
orderoo 
isoo 
notoo 
nulloo 
)oo 
{pp 	-
!AddSQLServerOrderDetailsToDtoListqq -
(qq- .

automapperqq. 8
,qq8 9
orderqq: ?
,qq? @
orederDetailsqqA N
)qqN O
;qqO P
}rr 	
returntt 
orederDetailstt 
;tt 
}uu 
publicww 

asyncww 
Taskww #
RemoveGameFromCartAsyncww -
(ww- .
Guidww. 2

customerIdww3 =
,ww= >
stringww? E
gameKeywwF M
,wwM N
intwwO R
quantitywwS [
)ww[ \
{xx 
loggeryy 
.yy 
LogInformationyy 
(yy 
$stryy C
,yyC D
gameKeyyyE L
)yyL M
;yyM N
var{{ 
exisitngOrder{{ 
={{ 
await{{ !

unitOfWork{{" ,
.{{, -
OrderRepository{{- <
.{{< = 
GetByCustomerIdAsync{{= Q
({{Q R

customerId{{R \
){{\ ]
??{{^ `
throw{{a f
new{{g j
GamestoreException{{k }
({{} ~
$"	{{~ �
$str
{{� �
{
{{� �

customerId
{{� �
}
{{� �
"
{{� �
)
{{� �
;
{{� �
var|| 
	orderGame|| 
=|| 
exisitngOrder|| %
.||% &

OrderGames||& 0
.||0 1
Find||1 5
(||5 6
x||6 7
=>||8 :
x||; <
.||< =
Game||= A
.||A B
Key||B E
==||F H
gameKey||I P
)||P Q
;||Q R
if~~ 

(~~ 
	orderGame~~ 
!=~~ 
null~~ 
)~~ 
{ 	
var
�� 
expectedQuantity
��  
=
��! "
	orderGame
��# ,
.
��, -
Quantity
��- 5
-
��6 7
quantity
��8 @
;
��@ A
if
�� 
(
�� 
expectedQuantity
��  
<=
��! #
$num
��$ %
)
��% &
{
�� 

unitOfWork
�� 
.
�� !
OrderGameRepository
�� .
.
��. /
Delete
��/ 5
(
��5 6
	orderGame
��6 ?
)
��? @
;
��@ A
}
�� 
else
�� 
{
�� 
	orderGame
�� 
.
�� 
Quantity
�� "
=
��# $
expectedQuantity
��% 5
;
��5 6
await
�� 

unitOfWork
��  
.
��  !!
OrderGameRepository
��! 4
.
��4 5
UpdateAsync
��5 @
(
��@ A
	orderGame
��A J
)
��J K
;
��K L
}
�� 
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
if
�� 
(
�� 
exisitngOrder
�� 
.
�� 

OrderGames
�� (
.
��( )
Count
��) .
==
��/ 1
$num
��2 3
)
��3 4
{
�� 

unitOfWork
�� 
.
�� 
OrderRepository
�� *
.
��* +
Delete
��+ 1
(
��1 2
exisitngOrder
��2 ?
)
��? @
;
��@ A
await
�� 

unitOfWork
��  
.
��  !
	SaveAsync
��! *
(
��* +
)
��+ ,
;
��, -
}
�� 
}
�� 	
else
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$"
��) +
$str
��+ N
{
��N O
gameKey
��O V
}
��V W
"
��W X
)
��X Y
;
��Y Z
}
�� 	
}
�� 
public
�� 

async
�� 
Task
�� 
<
�� 
byte
�� 
[
�� 
]
�� 
>
�� 
CreateInvoicePdf
�� .
(
��. /
PaymentModelDto
��/ >
payment
��? F
,
��F G
CustomerDto
��H S
customer
��T \
)
��\ ]
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� ;
,
��; <
payment
��= D
)
��D E
;
��E F
var
�� 
order
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
OrderRepository
��% 4
.
��4 5"
GetByCustomerIdAsync
��5 I
(
��I J
customer
��J R
.
��R S
Id
��S U
)
��U V
??
��W Y
throw
��Z _
new
��` c 
GamestoreException
��d v
(
��v w
$str��w �
)��� �
;��� �
var
�� 
sum
�� 
=
�� 
await
�� "
CalculateAmountToPay
�� ,
(
��, -

unitOfWork
��- 7
,
��7 8
customer
��9 A
)
��A B
;
��B C
QuestPDF
�� 
.
�� 
Settings
�� 
.
�� 
License
�� !
=
��" #
LicenseType
��$ /
.
��/ 0
	Community
��0 9
;
��9 :
var
�� 
document
�� 
=
�� 
new
�� 
Invoice
�� "
(
��" #
order
��# (
,
��( )
(
��* +
double
��+ 1
)
��1 2
sum
��2 5
)
��5 6
;
��6 7
byte
�� 
[
�� 
]
�� 
pdfBytes
�� 
=
�� 
document
�� "
.
��" #
GeneratePdf
��# .
(
��. /
)
��/ 0
;
��0 1
return
�� 
pdfBytes
�� 
;
�� 
}
�� 
public
�� 

async
�� 
Task
�� 
PayWithIboxAsync
�� &
(
��& '
PaymentModelDto
��' 6
payment
��7 >
,
��> ?
CustomerDto
��@ K
customer
��L T
)
��T U
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� D
,
��D E
payment
��F M
)
��M N
;
��N O
var
�� 
order
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
OrderRepository
��% 4
.
��4 5"
GetByCustomerIdAsync
��5 I
(
��I J
customer
��J R
.
��R S
Id
��S U
)
��U V
??
��W Y
throw
��Z _
new
��` c 
GamestoreException
��d v
(
��v w
$str��w �
)��� �
;��� �
var
�� 
iboxPaymentModel
�� 
=
�� 
await
�� $$
CreateIboxPaymentModel
��% ;
(
��; <

unitOfWork
��< F
,
��F G
customer
��H P
,
��P Q
order
��R W
)
��W X
;
��X Y
string
�� 

serviceUrl
�� 
=
�� )
paymentServiceConfiguration
�� 7
.
��7 8
Value
��8 =
.
��= >
IboxServiceUrl
��> L
;
��L M
await
�� '
MakePaymentServiceRequest
�� '
(
��' (
iboxPaymentModel
��( 8
,
��8 9

serviceUrl
��: D
)
��D E
;
��E F
await
�� &
ProcessOrderAfterPayment
�� &
(
��& '

unitOfWork
��' 1
,
��1 2
customer
��3 ;
)
��; <
;
��< =
}
�� 
public
�� 

async
�� 
Task
�� 
PayWithVisaAsync
�� &
(
��& '
PaymentModelDto
��' 6
payment
��7 >
,
��> ?
CustomerDto
��@ K
customer
��L T
)
��T U
{
�� 
logger
�� 
.
�� 
LogInformation
�� 
(
�� 
$str
�� J
,
��J K
payment
��L S
.
��S T
Model
��T Y
)
��Y Z
;
��Z [
await
�� #
_visaPaymentValidator
�� #
.
��# $!
ValidateVisaPayment
��$ 7
(
��7 8
payment
��8 ?
)
��? @
;
��@ A
var
�� 
visaPaymentModel
�� 
=
�� 
await
�� $$
CreateVisaPaymentModel
��% ;
(
��; <

unitOfWork
��< F
,
��F G

automapper
��H R
,
��R S
payment
��T [
,
��[ \
customer
��] e
)
��e f
;
��f g
string
�� 

serviceUrl
�� 
=
�� )
paymentServiceConfiguration
�� 7
.
��7 8
Value
��8 =
.
��= >
VisaServiceUrl
��> L
;
��L M
await
�� '
MakePaymentServiceRequest
�� '
(
��' (
visaPaymentModel
��( 8
,
��8 9

serviceUrl
��: D
)
��D E
;
��E F
await
�� &
ProcessOrderAfterPayment
�� &
(
��& '

unitOfWork
��' 1
,
��1 2
customer
��3 ;
)
��; <
;
��< =
}
�� 
public
�� 

PaymentMethodsDto
�� 
GetPaymentMethods
�� .
(
��. /
)
��/ 0
{
�� 
var
�� 
paymentMethods
�� 
=
�� 
new
��  
PaymentMethodsDto
��! 2
{
�� 	
PaymentMethods
�� 
=
�� 
[
�� 
new
�� 
(
�� 
)
�� 
{
�� 
Title
�� 
=
�� 
$str
�� "
,
��" #
Description
�� 
=
��  !
$str
��" 1
,
��1 2
ImageUrl
�� 
=
�� 
$str
�� u
,
��u v
}
�� 
,
�� 
new
�� 
(
�� 
)
�� 
{
�� 
Title
�� 
=
�� 
$str
�� "
,
��" #
Description
�� 
=
��  !
$str
��" 0
,
��0 1
ImageUrl
�� 
=
�� 
$str
�� X
,
��X Y
}
�� 
,
�� 
new
�� 
(
�� 
)
�� 
{
�� 
Title
�� 
=
�� 
$str
�� +
,
��+ ,
Description
�� 
=
��  !
$str
��" 0
,
��0 1
ImageUrl
�� 
=
�� 
$str
�� e
,
��e f
}
�� 
,
�� 
]
�� 
,
�� 
}
�� 	
;
��	 

return
�� 
paymentMethods
�� 
;
�� 
}
�� 
public
�� 

async
�� 
Task
�� 
	ShipAsync
�� 
(
��  
string
��  &
id
��' )
)
��) *
{
�� 
var
�� 
order
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
OrderRepository
��% 4
.
��4 5
GetByIdAsync
��5 A
(
��A B
new
��B E
Guid
��F J
(
��J K
id
��K M
)
��M N
)
��N O
;
��O P
order
�� 
.
�� 
Status
�� 
=
�� 
OrderStatus
�� "
.
��" #
Shipped
��# *
;
��* +
await
�� 

unitOfWork
�� 
.
�� 
OrderRepository
�� (
.
��( )
UpdateAsync
��) 4
(
��4 5
order
��5 :
)
��: ;
;
��; <
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
public
�� 

async
�� 
Task
�� $
AddProductToOrderAsync
�� ,
(
��, -
string
��- 3
orderId
��4 ;
,
��; <
string
��= C

productKey
��D N
)
��N O
{
�� 
var
�� 
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetGameByKeyAsync
��3 D
(
��D E

productKey
��E O
)
��O P
;
��P Q
if
�� 

(
�� 
game
�� 
==
�� 
null
�� 
)
�� 
{
�� 	
var
�� 
gameFromMongoDB
�� 
=
��  !
await
��" '"
MongoDbHelperService
��( <
.
��< =5
'GetGameWithDetailsFromMongoDBByKeyAsync
��= d
(
��d e
mongoUnitOfWork
��e t
,
��t u

automapper��v �
,��� �

productKey��� �
)��� �
;��� �
await
�� $
SqlServerHelperService
�� (
.
��( )C
5CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync
��) ^
(
��^ _

unitOfWork
��_ i
,
��i j

automapper
��k u
,
��u v
gameFromMongoDB��w �
,��� �
game��� �
)��� �
;��� �
game
�� 
=
�� 
await
�� 

unitOfWork
�� #
.
��# $
GameRepository
��$ 2
.
��2 3
GetGameByKeyAsync
��3 D
(
��D E

productKey
��E O
)
��O P
;
��P Q
}
�� 	
var
�� 
gameId
�� 
=
�� 
game
�� 
.
�� 
Id
�� 
;
�� 
await
�� "
CreateOrderGameAsync
�� "
(
��" #

unitOfWork
��# -
,
��- .
orderId
��/ 6
,
��6 7
game
��8 <
)
��< =
;
��= >
await
�� "
SetProductCountAsync
�� "
(
��" #

unitOfWork
��# -
,
��- .
orderId
��/ 6
,
��6 7
gameId
��8 >
)
��> ?
;
��? @
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� "
SetProductCountAsync
�� 2
(
��2 3
IUnitOfWork
��3 >

unitOfWork
��? I
,
��I J
string
��K Q
orderId
��R Y
,
��Y Z
Guid
��[ _
gameId
��` f
)
��f g
{
�� 
var
�� 
order
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
OrderRepository
��% 4
.
��4 5
GetByIdAsync
��5 A
(
��A B
new
��B E
Guid
��F J
(
��J K
orderId
��K R
)
��R S
)
��S T
;
��T U
order
�� 
.
�� 

OrderGames
�� 
.
�� 
First
�� 
(
�� 
x
��  
=>
��! #
x
��$ %
.
��% &
OrderId
��& -
==
��. 0
new
��1 4
Guid
��5 9
(
��9 :
orderId
��: A
)
��A B
&&
��C E
x
��F G
.
��G H
GameId
��H N
==
��O Q
gameId
��R X
)
��X Y
.
��Y Z
Quantity
��Z b
=
��c d
$num
��e f
;
��f g
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� "
CreateOrderGameAsync
�� 2
(
��2 3
IUnitOfWork
��3 >

unitOfWork
��? I
,
��I J
string
��K Q
orderId
��R Y
,
��Y Z
Game
��[ _
game
��` d
)
��d e
{
�� 
await
�� 

unitOfWork
�� 
.
�� !
OrderGameRepository
�� ,
.
��, -
AddAsync
��- 5
(
��5 6
new
��6 9
	OrderGame
��: C
(
��C D
)
��D E
{
��F G
OrderId
��H O
=
��P Q
new
��R U
Guid
��V Z
(
��Z [
orderId
��[ b
)
��b c
,
��c d
GameId
��e k
=
��l m
game
��n r
.
��r s
Id
��s u
,
��u v
Price
��w |
=
��} ~
game�� �
.��� �
Price��� �
,��� �
Discount��� �
=��� �
game��� �
.��� �
Discount��� �
}��� �
)��� �
;��� �
}
�� 
private
�� 
static
�� 
void
�� 
DeleteOrderGames
�� (
(
��( )
IUnitOfWork
��) 4

unitOfWork
��5 ?
,
��? @
Order
��A F
?
��F G
order
��H M
)
��M N
{
�� 
foreach
�� 
(
�� 
var
�� 
item
�� 
in
�� 
order
�� "
.
��" #

OrderGames
��# -
)
��- .
{
�� 	

unitOfWork
�� 
.
�� !
OrderGameRepository
�� *
.
��* +
Delete
��+ 1
(
��1 2
item
��2 6
)
��6 7
;
��7 8
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
DeleteOrder
�� )
(
��) *
IUnitOfWork
��* 5

unitOfWork
��6 @
,
��@ A
Order
��B G
order
��H M
)
��M N
{
�� 
DeleteOrderGames
�� 
(
�� 

unitOfWork
�� #
,
��# $
order
��% *
)
��* +
;
��+ ,

unitOfWork
�� 
.
�� 
OrderRepository
�� "
.
��" #
Delete
��# )
(
��) *
order
��* /
)
��/ 0
;
��0 1
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� &
ProcessOrderAfterPayment
�� 6
(
��6 7
IUnitOfWork
��7 B

unitOfWork
��C M
,
��M N
CustomerDto
��O Z
customer
��[ c
)
��c d
{
�� 
var
�� 
order
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
OrderRepository
��% 4
.
��4 5"
GetByCustomerIdAsync
��5 I
(
��I J
customer
��J R
.
��R S
Id
��S U
)
��U V
;
��V W
if
�� 

(
�� 
order
�� 
!=
�� 
null
�� 
)
�� 
{
�� 	
await
�� -
SetOrderStatusToPaidInSQLServer
�� 1
(
��1 2

unitOfWork
��2 <
,
��< =
order
��> C
)
��C D
;
��D E
await
�� -
UpdateProductQuanityInSQLServer
�� 1
(
��1 2

unitOfWork
��2 <
,
��< =
order
��> C
)
��C D
;
��D E
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� -
SetOrderStatusToPaidInSQLServer
�� =
(
��= >
IUnitOfWork
��> I

unitOfWork
��J T
,
��T U
Order
��V [
?
��[ \
order
��] b
)
��b c
{
�� 
order
�� 
.
�� 
Status
�� 
=
�� 
OrderStatus
�� "
.
��" #
Paid
��# '
;
��' (
await
�� 

unitOfWork
�� 
.
�� 
OrderRepository
�� (
.
��( )
UpdateAsync
��) 4
(
��4 5
order
��5 :
)
��: ;
;
��; <
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� -
UpdateProductQuanityInSQLServer
�� =
(
��= >
IUnitOfWork
��> I

unitOfWork
��J T
,
��T U
Order
��V [
?
��[ \
order
��] b
)
��b c
{
�� 
var
�� 

gameOrders
�� 
=
�� 
await
�� 

unitOfWork
�� )
.
��) *!
OrderGameRepository
��* =
.
��= >
GetByOrderIdAsync
��> O
(
��O P
order
��P U
.
��U V
Id
��V X
)
��X Y
;
��Y Z
foreach
�� 
(
�� 
var
�� 
	gameOrder
�� 
in
�� !

gameOrders
��" ,
)
��, -
{
�� 	
var
�� 
product
�� 
=
�� 
await
�� 

unitOfWork
��  *
.
��* +
GameRepository
��+ 9
.
��9 :
GetByIdAsync
��: F
(
��F G
	gameOrder
��G P
.
��P Q
GameId
��Q W
)
��W X
;
��X Y
if
�� 
(
�� 
product
�� 
!=
�� 
null
�� 
)
��  
{
�� 
product
�� 
.
�� 
UnitInStock
�� #
-=
��$ &
	gameOrder
��' 0
.
��0 1
Quantity
��1 9
;
��9 :
}
�� 
}
�� 	
}
�� 
private
�� 
static
�� 
void
�� /
!AddSQLServerOrderDetailsToDtoList
�� 9
(
��9 :
IMapper
��: A

automapper
��B L
,
��L M
Order
��N S
?
��S T
order
��U Z
,
��Z [
List
��\ `
<
��` a
OrderDetailsDto
��a p
>
��p q
orderDetails
��r ~
)
��~ 
{
�� 
if
�� 

(
�� 
order
�� 
.
�� 

OrderGames
�� 
.
�� 
Count
�� "
>
��# $
$num
��% &
)
��& '
{
�� 	
orderDetails
�� 
.
�� 
AddRange
�� !
(
��! "

automapper
��" ,
.
��, -
Map
��- 0
<
��0 1
List
��1 5
<
��5 6
OrderDetailsDto
��6 E
>
��E F
>
��F G
(
��G H
order
��H M
.
��M N

OrderGames
��N X
)
��X Y
)
��Y Z
;
��Z [
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� -
AddMongoDBOrderDetailsToDtoList
�� =
(
��= >
IMongoUnitOfWork
��> N
mongoUnitOfWork
��O ^
,
��^ _
IMapper
��` g

automapper
��h r
,
��r s
Order
��t y
order
��z 
,�� �
List��� �
<��� �
OrderDetailsDto��� �
>��� �
orderDetails��� �
)��� �
{
�� 
int
�� 
id
�� 
=
�� 
GuidHelpers
�� 
.
�� 
	GuidToInt
�� &
(
��& '
order
��' ,
.
��, -
Id
��- /
)
��/ 0
;
��0 1
try
�� 
{
�� 	
var
�� 
mongoOrderDetails
�� !
=
��" #
await
��$ )
mongoUnitOfWork
��* 9
.
��9 :#
OrderDetailRepository
��: O
.
��O P
GetByOrderIdAsync
��P a
(
��a b
id
��b d
)
��d e
;
��e f
order
�� 
.
�� 

OrderGames
�� 
=
�� 
[
��  
]
��  !
;
��! "
foreach
�� 
(
�� 
var
�� 
od
�� 
in
�� 
mongoOrderDetails
�� 0
)
��0 1
{
�� 
order
�� 
.
�� 

OrderGames
��  
.
��  !
Add
��! $
(
��$ %
new
��% (
	OrderGame
��) 2
(
��2 3
)
��3 4
{
��5 6
OrderId
��7 >
=
��? @
order
��A F
.
��F G
Id
��G I
,
��I J
GameId
��K Q
=
��R S
GuidHelpers
��T _
.
��_ `
	IntToGuid
��` i
(
��i j
od
��j l
.
��l m
	ProductId
��m v
)
��v w
,
��w x
Price
��y ~
=�� �
od��� �
.��� �
	UnitPrice��� �
,��� �
Quantity��� �
=��� �
od��� �
.��� �
Quantity��� �
,��� �
Discount��� �
=��� �
od��� �
.��� �
Discount��� �
}��� �
)��� �
;��� �
}
�� 
orderDetails
�� 
.
�� 
AddRange
�� !
(
��! "

automapper
��" ,
.
��, -
Map
��- 0
<
��0 1
List
��1 5
<
��5 6
OrderDetailsDto
��6 E
>
��E F
>
��F G
(
��G H
order
��H M
.
��M N

OrderGames
��N X
)
��X Y
)
��Y Z
;
��Z [
}
�� 	
catch
�� 
(
�� 
	Exception
�� 
)
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$str
��) m
)
��m n
;
��n o
}
�� 	
}
�� 
private
�� 
static
�� 
void
�� )
AddSQLServerOrdersToDtoList
�� 3
(
��3 4
IMapper
��4 ;

automapper
��< F
,
��F G
List
��H L
<
��L M
Order
��M R
>
��R S
orders
��T Z
,
��Z [
List
��\ `
<
��` a
OrderModelDto
��a n
>
��n o
orderModels
��p {
)
��{ |
{
�� 
orderModels
�� 
.
�� 
AddRange
�� 
(
�� 

automapper
�� '
.
��' (
Map
��( +
<
��+ ,
List
��, 0
<
��0 1
OrderModelDto
��1 >
>
��> ?
>
��? @
(
��@ A
orders
��A G
)
��G H
)
��H I
;
��I J
}
�� 
private
�� 
static
�� 
void
�� %
AddMongoOrdersToDtoList
�� /
(
��/ 0
IMapper
��0 7

automapper
��8 B
,
��B C
List
��D H
<
��H I

MongoOrder
��I S
>
��S T
ordersFromMongo
��U d
,
��d e
DateTime
��f n
startD
��o u
,
��u v
DateTime
��w 
endD��� �
,��� �
List��� �
<��� �
OrderModelDto��� �
>��� �
orderModels��� �
)��� �
{
�� 
var
�� &
mongoOrdersWitheDateTime
�� $
=
��% &

automapper
��' 1
.
��1 2
Map
��2 5
<
��5 6
List
��6 :
<
��: ;
MongoOrderModel
��; J
>
��J K
>
��K L
(
��L M
ordersFromMongo
��M \
)
��\ ]
;
��] ^
var
�� $
mongoOrdersByDateRange
�� "
=
��# $&
mongoOrdersWitheDateTime
��% =
.
��= >
Where
��> C
(
��C D
x
��D E
=>
��F H
x
��I J
.
��J K
Date
��K O
>=
��P R
startD
��S Y
&&
��Z \
x
��] ^
.
��^ _
Date
��_ c
<=
��d f
endD
��g k
)
��k l
;
��l m
var
�� !
filteredMOngoOrders
�� 
=
��  !

automapper
��" ,
.
��, -
Map
��- 0
<
��0 1
List
��1 5
<
��5 6
OrderModelDto
��6 C
>
��C D
>
��D E
(
��E F$
mongoOrdersByDateRange
��F \
)
��\ ]
;
��] ^
orderModels
�� 
.
�� 
AddRange
�� 
(
�� !
filteredMOngoOrders
�� 0
)
��0 1
;
��1 2
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
double
�� $
?
��$ %
>
��% &"
CalculateAmountToPay
��' ;
(
��; <
IUnitOfWork
��< G

unitOfWork
��H R
,
��R S
CustomerDto
��T _
customer
��` h
)
��h i
{
�� 
var
�� 
order
�� 
=
�� 
await
�� 

unitOfWork
�� $
.
��$ %
OrderRepository
��% 4
.
��4 5"
GetByCustomerIdAsync
��5 I
(
��I J
customer
��J R
.
��R S
Id
��S U
)
��U V
;
��V W
var
�� 

orderGames
�� 
=
�� 
await
�� 

unitOfWork
�� )
.
��) *!
OrderGameRepository
��* =
.
��= >
GetByOrderIdAsync
��> O
(
��O P
order
��P U
.
��U V
Id
��V X
)
��X Y
;
��Y Z
double
�� 
?
�� 
sum
�� 
=
�� 
$num
�� 
;
�� 
foreach
�� 
(
�� 
var
�� 
	orderGame
�� 
in
�� !

orderGames
��" ,
)
��, -
{
�� 	
if
�� 
(
�� 
	orderGame
�� 
.
�� 
Discount
�� "
is
��# %
not
��& )
null
��* .
and
��/ 2
not
��3 6
$num
��7 8
)
��8 9
{
�� 
sum
�� 
+=
�� 
(
�� 
	orderGame
�� !
.
��! "
Price
��" '
-
��( )
(
��* +
	orderGame
��+ 4
.
��4 5
Price
��5 :
*
��; <
(
��= >
(
��> ?
double
��? E
)
��E F
	orderGame
��F O
.
��O P
Discount
��P X
/
��Y Z
$num
��[ ^
)
��^ _
)
��_ `
)
��` a
*
��b c
	orderGame
��d m
.
��m n
Quantity
��n v
;
��v w
}
�� 
else
�� 
{
�� 
sum
�� 
+=
�� 
	orderGame
��  
.
��  !
Price
��! &
;
��& '
}
�� 
}
�� 	
return
�� 
sum
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� '
MakePaymentServiceRequest
�� 7
(
��7 8
object
��8 >
paymentModel
��? K
,
��K L
string
��M S

serviceUrl
��T ^
)
��^ _
{
�� 
var
�� 
json
�� 
=
�� 
JsonSerializer
�� !
.
��! "
	Serialize
��" +
(
��+ ,
paymentModel
��, 8
)
��8 9
;
��9 :
var
�� 
content
�� 
=
�� 
new
�� 
StringContent
�� '
(
��' (
json
��( ,
,
��, -
Encoding
��. 6
.
��6 7
UTF8
��7 ;
,
��; <
$str
��= O
)
��O P
;
��P Q

HttpClient
�� 
client
�� 
=
�� 
new
�� 

HttpClient
��  *
(
��* +
)
��+ ,
;
��, -
var
�� 
response
�� 
=
�� 
await
�� 
client
�� #
.
��# $
	PostAsync
��$ -
(
��- .

serviceUrl
��. 8
,
��8 9
content
��: A
)
��A B
;
��B C
response
�� 
.
�� %
EnsureSuccessStatusCode
�� (
(
��( )
)
��) *
;
��* +
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� *
VisaMicroservicePaymentModel
�� :
>
��: ;$
CreateVisaPaymentModel
��< R
(
��R S
IUnitOfWork
��S ^

unitOfWork
��_ i
,
��i j
IMapper
��k r

automapper
��s }
,
��} ~
PaymentModelDto�� �
payment��� �
,��� �
CustomerDto��� �
customer��� �
)��� �
{
�� 
var
�� 
visaPaymentModel
�� 
=
�� 

automapper
�� )
.
��) *
Map
��* -
<
��- .*
VisaMicroservicePaymentModel
��. J
>
��J K
(
��K L
payment
��L S
)
��S T
;
��T U
var
�� 
sum
�� 
=
�� 
await
�� "
CalculateAmountToPay
�� ,
(
��, -

unitOfWork
��- 7
,
��7 8
customer
��9 A
)
��A B
;
��B C
visaPaymentModel
�� 
.
�� 
TransactionAmount
�� *
=
��+ ,
(
��- .
double
��. 4
)
��4 5
sum
��5 8
;
��8 9
return
�� 
visaPaymentModel
�� 
;
��  
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
IboxPaymentModel
�� .
>
��. /$
CreateIboxPaymentModel
��0 F
(
��F G
IUnitOfWork
��G R

unitOfWork
��S ]
,
��] ^
CustomerDto
��_ j
customer
��k s
,
��s t
Order
��u z
order��{ �
)��� �
{
�� 
var
�� 
iboxPaymentModel
�� 
=
�� 
new
�� "
IboxPaymentModel
��# 3
(
��3 4
)
��4 5
{
�� 	
InvoiceNumber
�� 
=
�� 
order
�� !
.
��! "
Id
��" $
,
��$ %
AccountNumber
�� 
=
�� 
customer
�� $
.
��$ %
Id
��% '
,
��' (
}
�� 	
;
��	 

var
�� 
sum
�� 
=
�� 
await
�� "
CalculateAmountToPay
�� ,
(
��, -

unitOfWork
��- 7
,
��7 8
customer
��9 A
)
��A B
;
��B C
iboxPaymentModel
�� 
.
�� 
TransactionAmount
�� *
=
��+ ,
(
��- .
decimal
��. 5
?
��5 6
)
��6 7
sum
��7 :
;
��: ;
return
�� 
iboxPaymentModel
�� 
;
��  
}
�� 
private
�� 
static
�� 
void
�� ,
ParseDateRangeToDateTimeFormat
�� 6
(
��6 7
ref
��7 :
string
��; A
?
��A B
	startDate
��C L
,
��L M
ref
��N Q
string
��R X
?
��X Y
endDate
��Z a
,
��a b
out
��c f
DateTime
��g o
startD
��p v
,
��v w
out
��x {
DateTime��| �
endD��� �
)��� �
{
�� 
string
�� 
format
�� 
=
�� 
$str
�� %
;
��% &
startD
�� 
=
�� 
DateTime
�� 
.
�� 
Now
�� 
.
�� 
AddYears
�� &
(
��& '
-
��' (
$num
��( ,
)
��, -
;
��- .
endD
�� 
=
�� 
DateTime
�� 
.
�� 
Now
�� 
;
�� 
try
�� 
{
�� 	
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� %
(
��% &
	startDate
��& /
)
��/ 0
)
��0 1
{
�� 
	startDate
�� 
=
�� 
	startDate
�� %
.
��% &
	Substring
��& /
(
��/ 0
$num
��0 1
,
��1 2
$num
��3 5
)
��5 6
;
��6 7
startD
�� 
=
�� 
DateTime
�� !
.
��! "

ParseExact
��" ,
(
��, -
	startDate
��- 6
,
��6 7
format
��8 >
,
��> ?
CultureInfo
��@ K
.
��K L
InvariantCulture
��L \
,
��\ ]
DateTimeStyles
��^ l
.
��l m
AdjustToUniversal
��m ~
)
��~ 
;�� �
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� )
(
��) *
endDate
��* 1
)
��1 2
)
��2 3
{
�� 
endDate
�� 
=
�� 
endDate
�� %
.
��% &
	Substring
��& /
(
��/ 0
$num
��0 1
,
��1 2
$num
��3 5
)
��5 6
;
��6 7
endD
�� 
=
�� 
DateTime
�� #
.
��# $

ParseExact
��$ .
(
��. /
endDate
��/ 6
,
��6 7
format
��8 >
,
��> ?
CultureInfo
��@ K
.
��K L
InvariantCulture
��L \
,
��\ ]
DateTimeStyles
��^ l
.
��l m
AdjustToUniversal
��m ~
)
��~ 
;�� �
}
�� 
}
�� 
}
�� 	
catch
�� 
(
�� 
FormatException
�� 
)
�� 
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$str
��) U
)
��U V
;
��V W
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
Order
�� #
?
��# $
>
��$ %$
GetOrderFromMongDBById
��& <
(
��< =
IMongoUnitOfWork
��= M
mongoUnitOfWork
��N ]
,
��] ^
IMapper
��_ f

automapper
��g q
,
��q r
Guid
��s w
orderId
��x 
)�� �
{
�� 
int
�� 
id
�� 
=
�� 
GuidHelpers
�� 
.
�� 
	GuidToInt
�� &
(
��& '
orderId
��' .
)
��. /
;
��/ 0
var
�� 
o
�� 
=
�� 
await
�� 
mongoUnitOfWork
�� %
.
��% &
OrderRepository
��& 5
.
��5 6
GetByIdAsync
��6 B
(
��B C
id
��C E
)
��E F
;
��F G
var
�� 
order
�� 
=
�� 

automapper
�� 
.
�� 
Map
�� "
<
��" #
Order
��# (
>
��( )
(
��) *
o
��* +
)
��+ ,
;
��, -
return
�� 
order
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
Order
�� #
?
��# $
>
��$ %'
GetOrderFromSQLServerById
��& ?
(
��? @
IUnitOfWork
��@ K

unitOfWork
��L V
,
��V W
Guid
��X \
orderId
��] d
)
��d e
{
�� 
return
�� 
await
�� 

unitOfWork
�� 
.
��  
OrderRepository
��  /
.
��/ 0
GetByIdAsync
��0 <
(
��< =
orderId
��= D
)
��D E
;
��E F
}
�� 
}�� �G
gD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\PlatformService.cs
	namespace 	
	Gamestore
 
. 
Services 
. 
Services %
;% &
public 
class 
PlatformService 
( 
IUnitOfWork (

unitOfWork) 3
,3 4
IMongoUnitOfWork5 E
mongoUnitOfWorkF U
,U V
IMapperW ^

automapper_ i
,i j
ILoggerk r
<r s
PlatformService	s �
>
� �
logger
� �
)
� �
:
� �
IPlatformService
� �
{ 
private 
const 
string 
PhysicalProductType ,
=- .
$str/ A
;A B
private 
readonly '
PlatformDtoWrapperValidator 0(
_platformDtoWrapperValidator1 M
=N O
newP S
(S T

unitOfWorkT ^
)^ _
;_ `
public 

async 
Task 
< 
IEnumerable !
<! "
GameModelDto" .
>. /
>/ 0%
GetGamesByPlatformIdAsync1 J
(J K
GuidK O

platformIdP Z
)Z [
{ 
logger 
. 
LogInformation 
( 
$str J
,J K

platformIdL V
)V W
;W X
var 
games 
= 
await 

unitOfWork $
.$ %
PlatformRepository% 7
.7 8#
GetGamesByPlatformAsync8 O
(O P

platformIdP Z
)Z [
;[ \
List 
< 
GameModelDto 
> 

gameModels %
=& '

automapper( 2
.2 3
Map3 6
<6 7
List7 ;
<; <
GameModelDto< H
>H I
>I J
(J K
gamesK P
)P Q
;Q R
if 

( 

platformId 
== 
( 
await  

unitOfWork! +
.+ ,
PlatformRepository, >
.> ?
GetByTypeAsync? M
(M N
PhysicalProductTypeN a
)a b
)b c
.c d
Idd f
)f g
{ 	
var 
gamesFromMongoDB  
=! "

automapper# -
.- .
Map. 1
<1 2
List2 6
<6 7
GameModelDto7 C
>C D
>D E
(E F
awaitF K
mongoUnitOfWorkL [
.[ \
ProductRepository\ m
.m n
GetAllAsyncn y
(y z
)z {
){ |
.| }
Except	} �
(
� �

gameModels
� �
)
� �
;
� �

gameModels 
. 
AddRange 
(  
gamesFromMongoDB  0
)0 1
;1 2
} 	
return   

gameModels   
.   
AsEnumerable   &
(  & '
)  ' (
;  ( )
}!! 
public## 

async## 
Task## 
<## 
PlatformModelDto## &
>##& ' 
GetPlatformByIdAsync##( <
(##< =
Guid##= A

platformId##B L
)##L M
{$$ 
logger%% 
.%% 
LogInformation%% 
(%% 
$str%% D
,%%D E

platformId%%F P
)%%P Q
;%%Q R
var&& 
platform&& 
=&& 
await&& 

unitOfWork&& '
.&&' (
PlatformRepository&&( :
.&&: ;
GetByIdAsync&&; G
(&&G H

platformId&&H R
)&&R S
;&&S T
return(( 
platform(( 
==(( 
null(( 
?((  !
throw((" '
new((( +
GamestoreException((, >
(((> ?
$"((? A
$str((A b
{((b c

platformId((c m
}((m n
"((n o
)((o p
:((q r

automapper((s }
.((} ~
Map	((~ �
<
((� �
PlatformModelDto
((� �
>
((� �
(
((� �
platform
((� �
)
((� �
;
((� �
})) 
public++ 

async++ 
Task++ 
<++ 
IEnumerable++ !
<++! "
PlatformModelDto++" 2
>++2 3
>++3 4 
GetAllPlatformsAsync++5 I
(++I J
)++J K
{,, 
logger-- 
.-- 
LogInformation-- 
(-- 
$str-- 5
)--5 6
;--6 7
var.. 
	platforms.. 
=.. 
await.. 

unitOfWork.. (
...( )
PlatformRepository..) ;
...; <
GetAllAsync..< G
(..G H
)..H I
;..I J
List// 
<// 
PlatformModelDto// 
>// 
platformModels// -
=//. /

automapper//0 :
.//: ;
Map//; >
<//> ?
List//? C
<//C D
PlatformModelDto//D T
>//T U
>//U V
(//V W
	platforms//W `
)//` a
;//a b
return11 
platformModels11 
.11 
AsEnumerable11 *
(11* +
)11+ ,
;11, -
}22 
public44 

async44 
Task44 #
DeletePlatformByIdAsync44 -
(44- .
Guid44. 2

platformId443 =
)44= >
{55 
logger66 
.66 
LogInformation66 
(66 
$str66 E
,66E F

platformId66G Q
)66Q R
;66R S
var77 
platform77 
=77 
await77 

unitOfWork77 '
.77' (
PlatformRepository77( :
.77: ;
GetByIdAsync77; G
(77G H

platformId77H R
)77R S
;77S T
if88 

(88 
platform88 
!=88 
null88 
)88 
{99 	

unitOfWork:: 
.:: 
PlatformRepository:: )
.::) *
Delete::* 0
(::0 1
platform::1 9
)::9 :
;::: ;
await;; 

unitOfWork;; 
.;; 
	SaveAsync;; &
(;;& '
);;' (
;;;( )
}<< 	
else== 
{>> 	
throw?? 
new?? 
GamestoreException?? (
(??( )
$"??) +
$str??+ L
{??L M

platformId??M W
}??W X
"??X Y
)??Y Z
;??Z [
}@@ 	
}AA 
publicCC 

asyncCC 
TaskCC 
AddPlatformAsyncCC &
(CC& '
PlatformDtoWrapperCC' 9
platformModelCC: G
)CCG H
{DD 
loggerEE 
.EE 
LogInformationEE 
(EE 
$strEE <
,EE< =
platformModelEE> K
)EEK L
;EEL M
awaitGG (
_platformDtoWrapperValidatorGG *
.GG* +
ValidatePlatformGG+ ;
(GG; <
platformModelGG< I
)GGI J
;GGJ K
varII 
platformII 
=II 

automapperII !
.II! "
MapII" %
<II% &
PlatformII& .
>II. /
(II/ 0
platformModelII0 =
.II= >
PlatformII> F
)IIF G
;IIG H
awaitKK 

unitOfWorkKK 
.KK 
PlatformRepositoryKK +
.KK+ ,
AddAsyncKK, 4
(KK4 5
platformKK5 =
)KK= >
;KK> ?
awaitLL 

unitOfWorkLL 
.LL 
	SaveAsyncLL "
(LL" #
)LL# $
;LL$ %
}MM 
publicOO 

asyncOO 
TaskOO 
UpdatePlatformAsyncOO )
(OO) *
PlatformDtoWrapperOO* <
platformModelOO= J
)OOJ K
{PP 
loggerQQ 
.QQ 
LogInformationQQ 
(QQ 
$strQQ C
,QQC D
platformModelQQE R
)QQR S
;QQS T
awaitSS (
_platformDtoWrapperValidatorSS *
.SS* +
ValidatePlatformSS+ ;
(SS; <
platformModelSS< I
)SSI J
;SSJ K
varUU 
platformUU 
=UU 

automapperUU !
.UU! "
MapUU" %
<UU% &
PlatformUU& .
>UU. /
(UU/ 0
platformModelUU0 =
.UU= >
PlatformUU> F
)UUF G
;UUG H
awaitWW 

unitOfWorkWW 
.WW 
PlatformRepositoryWW +
.WW+ ,
UpdateAsyncWW, 7
(WW7 8
platformWW8 @
)WW@ A
;WWA B
awaitXX 

unitOfWorkXX 
.XX 
	SaveAsyncXX "
(XX" #
)XX# $
;XX$ %
}YY 
}ZZ ��
hD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\PublisherService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
public 
class 
PublisherService 
( 
IUnitOfWork )

unitOfWork* 4
,4 5
IMongoUnitOfWork6 F
mongoUnitOfWorkG V
,V W
IMapperX _

automapper` j
,j k
ILoggerl s
<s t
GameServicet 
>	 �
logger
� �
)
� �
:
� �
IPublisherService
� �
{ 
private 
readonly (
PublisherDtoWrapperValidator 1)
_publisherDtoWrapperValidator2 O
=P Q
newR U
(U V

unitOfWorkV `
)` a
;a b
public 

async 
Task #
DeletPublisherByIdAsync -
(- .
Guid. 2
publisherId3 >
)> ?
{ 
logger 
. 
LogInformation 
( 
$str @
,@ A
publisherIdB M
)M N
;N O
var 
	publisher 
= 
await 

unitOfWork (
.( )
PublisherRepository) <
.< =
GetByIdAsync= I
(I J
publisherIdJ U
)U V
;V W
if 

( 
	publisher 
!= 
null 
) 
{ 	

unitOfWork 
. 
PublisherRepository *
.* +
Delete+ 1
(1 2
	publisher2 ;
); <
;< =
await 

unitOfWork 
. 
	SaveAsync &
(& '
)' (
;( )
} 	
else 
{ 	
throw 
new 
GamestoreException (
(( )
$") +
$str+ M
{M N
publisherIdN Y
}Y Z
"Z [
)[ \
;\ ]
}   	
}!! 
public## 

async## 
Task## 
<## 
PublisherModelDto## '
>##' (!
GetPublisherByIdAsync##) >
(##> ?
Guid##? C
publisherId##D O
)##O P
{$$ 
logger%% 
.%% 
LogInformation%% 
(%% 
$str%% F
,%%F G
publisherId%%H S
)%%S T
;%%T U
var&& 
	publisher&& 
=&& 
await&& 

unitOfWork&& (
.&&( )
PublisherRepository&&) <
.&&< =
GetByIdAsync&&= I
(&&I J
publisherId&&J U
)&&U V
;&&V W
return(( 
	publisher(( 
==(( 
null((  
?((! "
throw((# (
new(() ,
GamestoreException((- ?
(((? @
$"((@ B
$str((B d
{((d e
publisherId((e p
}((p q
"((q r
)((r s
:((t u

automapper	((v �
.
((� �
Map
((� �
<
((� �
PublisherModelDto
((� �
>
((� �
(
((� �
	publisher
((� �
)
((� �
;
((� �
})) 
public++ 

async++ 
Task++ 
<++ 
PublisherModelDto++ '
>++' (*
GetPublisherByCompanyNameAsync++) G
(++G H
string++H N
companyName++O Z
)++Z [
{,, 
logger-- 
.-- 
LogInformation-- 
(-- 
$str-- O
,--O P
companyName--Q \
)--\ ]
;--] ^
var// 
	publisher// 
=// 
await// 2
&GetPublisherFromSQLServerByCompanyName// D
(//D E

unitOfWork//E O
,//O P
companyName//Q \
)//\ ]
;//] ^
	publisher00 
??=00 
await00 #
GetPublisherFromMongoDB00 3
(003 4
mongoUnitOfWork004 C
,00C D

automapper00E O
,00O P
companyName00Q \
)00\ ]
;00] ^
return22 
	publisher22 
==22 
null22  
?22! "
throw22# (
new22) ,
GamestoreException22- ?
(22? @
$"22@ B
$str22B n
{22n o
companyName22o z
}22z {
"22{ |
)22| }
:22~ 

automapper
22� �
.
22� �
Map
22� �
<
22� �
PublisherModelDto
22� �
>
22� �
(
22� �
	publisher
22� �
)
22� �
;
22� �
}33 
public55 

async55 
Task55 
<55 
IEnumerable55 !
<55! "
PublisherModelDto55" 3
>553 4
>554 5!
GetAllPublishersAsync556 K
(55K L
)55L M
{66 
logger77 
.77 
LogInformation77 
(77 
$str77 6
)776 7
;777 8
var99 
publisherModels99 
=99 
await99 #&
GetPublishersFromSQLServer99$ >
(99> ?

unitOfWork99? I
,99I J

automapper99K U
)99U V
;99V W
publisherModels:: 
.:: 
AddRange::  
(::  !
await::! &$
GetPublishersFromMongoDB::' ?
(::? @
mongoUnitOfWork::@ O
,::O P

automapper::Q [
)::[ \
)::\ ]
;::] ^
return<< 
publisherModels<< 
.<< 
AsEnumerable<< +
(<<+ ,
)<<, -
;<<- .
}== 
public?? 

async?? 
Task?? 
<?? 
IEnumerable?? !
<??! "
GameModelDto??" .
>??. /
>??/ 0&
GetGamesByPublisherIdAsync??1 K
(??K L
Guid??L P
publisherId??Q \
)??\ ]
{@@ 
loggerAA 
.AA 
LogInformationAA 
(AA 
$strAA I
,AAI J
publisherIdAAK V
)AAV W
;AAW X
varBB 
gamesBB 
=BB 
awaitBB 

unitOfWorkBB $
.BB$ %
PublisherRepositoryBB% 8
.BB8 9&
GetGamesByPublisherIdAsyncBB9 S
(BBS T
publisherIdBBT _
)BB_ `
;BB` a
varCC 

gameModelsCC 
=CC 

automapperCC #
.CC# $
MapCC$ '
<CC' (
ListCC( ,
<CC, -
GameModelDtoCC- 9
>CC9 :
>CC: ;
(CC; <
gamesCC< A
)CCA B
;CCB C
returnEE 

gameModelsEE 
.EE 
AsEnumerableEE &
(EE& '
)EE' (
;EE( )
}FF 
publicHH 

asyncHH 
TaskHH 
<HH 
IEnumerableHH !
<HH! "
GameModelDtoHH" .
>HH. /
>HH/ 0(
GetGamesByPublisherNameAsyncHH1 M
(HHM N
stringHHN T
publisherNameHHU b
)HHb c
{II 
loggerJJ 
.JJ 
LogInformationJJ 
(JJ 
$strJJ K
,JJK L
publisherNameJJM Z
)JJZ [
;JJ[ \
varLL 

gameModelsLL 
=LL 
awaitLL 0
$GetGamesByPublisherNameFromSQLServerLL C
(LLC D

unitOfWorkLLD N
,LLN O

automapperLLP Z
,LLZ [
publisherNameLL\ i
)LLi j
;LLj k
ifMM 

(MM 

gameModelsMM 
.MM 
CountMM 
==MM 
$numMM  !
)MM! "
{NN 	

gameModelsOO 
=OO 
awaitOO .
"GetGamesByPublisherNameFromMongoDBOO A
(OOA B
mongoUnitOfWorkOOB Q
,OOQ R

automapperOOS ]
,OO] ^
publisherNameOO_ l
)OOl m
;OOm n
}PP 	
returnRR 

gameModelsRR 
.RR 
AsEnumerableRR &
(RR& '
)RR' (
;RR( )
}SS 
publicUU 

asyncUU 
TaskUU 
AddPublisherAsyncUU '
(UU' (
PublisherDtoWrapperUU( ;
publisherModelUU< J
)UUJ K
{VV 
loggerWW 
.WW 
LogInformationWW 
(WW 
$strWW B
,WWB C
publisherModelWWD R
)WWR S
;WWS T
awaitYY )
_publisherDtoWrapperValidatorYY +
.YY+ ,
ValidatePublisherYY, =
(YY= >
publisherModelYY> L
)YYL M
;YYM N
var[[ 
	publisher[[ 
=[[ 

automapper[[ "
.[[" #
Map[[# &
<[[& '
	Publisher[[' 0
>[[0 1
([[1 2
publisherModel[[2 @
.[[@ A
	Publisher[[A J
)[[J K
;[[K L
await\\ 

unitOfWork\\ 
.\\ 
PublisherRepository\\ ,
.\\, -
AddAsync\\- 5
(\\5 6
	publisher\\6 ?
)\\? @
;\\@ A
await]] 

unitOfWork]] 
.]] 
	SaveAsync]] "
(]]" #
)]]# $
;]]$ %
}^^ 
public`` 

async`` 
Task``  
UpdatePublisherAsync`` *
(``* +
PublisherDtoWrapper``+ >
publisherModel``? M
)``M N
{aa 
loggerbb 
.bb 
LogInformationbb 
(bb 
$strbb D
,bbD E
publisherModelbbF T
)bbT U
;bbU V
awaitdd )
_publisherDtoWrapperValidatordd +
.dd+ ,
ValidatePublisherdd, =
(dd= >
publisherModeldd> L
)ddL M
;ddM N
varff 
	publisherff 
=ff 

automapperff "
.ff" #
Mapff# &
<ff& '
	Publisherff' 0
>ff0 1
(ff1 2
publisherModelff2 @
.ff@ A
	PublisherffA J
)ffJ K
;ffK L
awaithh 

unitOfWorkhh 
.hh 
PublisherRepositoryhh ,
.hh, -
UpdateAsynchh- 8
(hh8 9
	publisherhh9 B
)hhB C
;hhC D
awaitjj 

unitOfWorkjj 
.jj 
	SaveAsyncjj "
(jj" #
)jj# $
;jj$ %
}kk 
privatemm 
staticmm 
asyncmm 
Taskmm 
<mm 
	Publishermm '
?mm' (
>mm( )#
GetPublisherFromMongoDBmm* A
(mmA B
IMongoUnitOfWorkmmB R
mongoUnitOfWorkmmS b
,mmb c
IMappermmd k

automappermml v
,mmv w
stringmmx ~
companyName	mm �
)
mm� �
{nn 
varoo 
supplieroo 
=oo 
awaitoo 
mongoUnitOfWorkoo ,
.oo, -
SupplierRepositoryoo- ?
.oo? @
GetByNameAsyncoo@ N
(ooN O
companyNameooO Z
)ooZ [
;oo[ \
varpp 
	publisherpp 
=pp 

automapperpp "
.pp" #
Mappp# &
<pp& '
	Publisherpp' 0
>pp0 1
(pp1 2
supplierpp2 :
)pp: ;
;pp; <
returnqq 
	publisherqq 
;qq 
}rr 
privatett 
statictt 
asynctt 
Tasktt 
<tt 
	Publishertt '
?tt' (
>tt( )2
&GetPublisherFromSQLServerByCompanyNamett* P
(ttP Q
IUnitOfWorkttQ \

unitOfWorktt] g
,ttg h
stringtti o
companyNamettp {
)tt{ |
{uu 
returnvv 
awaitvv 

unitOfWorkvv 
.vv  
PublisherRepositoryvv  3
.vv3 4!
GetByCompanyNameAsyncvv4 I
(vvI J
companyNamevvJ U
)vvU V
;vvV W
}ww 
privateyy 
staticyy 
asyncyy 
Taskyy 
<yy 
Listyy "
<yy" #
GameModelDtoyy# /
>yy/ 0
>yy0 10
$GetGamesByPublisherNameFromSQLServeryy2 V
(yyV W
IUnitOfWorkyyW b

unitOfWorkyyc m
,yym n
IMapperyyo v

automapper	yyw �
,
yy� �
string
yy� �
publisherName
yy� �
)
yy� �
{zz 
var{{ 
games{{ 
={{ 
await{{ 

unitOfWork{{ $
.{{$ %
PublisherRepository{{% 8
.{{8 9(
GetGamesByPublisherNameAsync{{9 U
({{U V
publisherName{{V c
){{c d
;{{d e
var|| 

gameModels|| 
=|| 

automapper|| #
.||# $
Map||$ '
<||' (
List||( ,
<||, -
GameModelDto||- 9
>||9 :
>||: ;
(||; <
games||< A
)||A B
;||B C
return~~ 

gameModels~~ 
;~~ 
} 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
List
�� "
<
��" #
GameModelDto
��# /
>
��/ 0
>
��0 10
"GetGamesByPublisherNameFromMongoDB
��2 T
(
��T U
IMongoUnitOfWork
��U e
mongoUnitOfWork
��f u
,
��u v
IMapper
��w ~

automapper�� �
,��� �
string��� �
publisherName��� �
)��� �
{
�� 
var
�� 
supplier
�� 
=
�� 
await
�� 
mongoUnitOfWork
�� ,
.
��, - 
SupplierRepository
��- ?
.
��? @
GetByNameAsync
��@ N
(
��N O
publisherName
��O \
)
��\ ]
;
��] ^
var
�� 
games
�� 
=
�� 
await
�� 
mongoUnitOfWork
�� )
.
��) *
ProductRepository
��* ;
.
��; <"
GetBySupplierIdAsync
��< P
(
��P Q
supplier
��Q Y
.
��Y Z

SupplierID
��Z d
)
��d e
;
��e f
var
�� 

gameModels
�� 
=
�� 

automapper
�� #
.
��# $
Map
��$ '
<
��' (
List
��( ,
<
��, -
GameModelDto
��- 9
>
��9 :
>
��: ;
(
��; <
games
��< A
)
��A B
;
��B C
return
�� 

gameModels
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
List
�� "
<
��" #
PublisherModelDto
��# 4
>
��4 5
>
��5 6(
GetPublishersFromSQLServer
��7 Q
(
��Q R
IUnitOfWork
��R ]

unitOfWork
��^ h
,
��h i
IMapper
��j q

automapper
��r |
)
��| }
{
�� 
var
�� 

publishers
�� 
=
�� 
await
�� 

unitOfWork
�� )
.
��) *!
PublisherRepository
��* =
.
��= >
GetAllAsync
��> I
(
��I J
)
��J K
;
��K L
var
�� 
publisherModels
�� 
=
�� 

automapper
�� (
.
��( )
Map
��) ,
<
��, -
List
��- 1
<
��1 2
PublisherModelDto
��2 C
>
��C D
>
��D E
(
��E F

publishers
��F P
)
��P Q
;
��Q R
return
�� 
publisherModels
�� 
;
�� 
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� 
<
�� 
List
�� "
<
��" #
PublisherModelDto
��# 4
>
��4 5
>
��5 6&
GetPublishersFromMongoDB
��7 O
(
��O P
IMongoUnitOfWork
��P `
mongoUnitOfWork
��a p
,
��p q
IMapper
��r y

automapper��z �
)��� �
{
�� 
var
�� 
	suppliers
�� 
=
�� 
await
�� 
mongoUnitOfWork
�� -
.
��- . 
SupplierRepository
��. @
.
��@ A
GetAllAsync
��A L
(
��L M
)
��M N
;
��N O
var
�� 

publishers
�� 
=
�� 

automapper
�� #
.
��# $
Map
��$ '
<
��' (
List
��( ,
<
��, -
PublisherModelDto
��- >
>
��> ?
>
��? @
(
��@ A
	suppliers
��A J
)
��J K
;
��K L
return
�� 

publishers
�� 
;
�� 
}
�� 
}�� �e
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\RoleService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
public 
class 
RoleService 
( 
IIdentityUnitOfWork ,

unitOfWork- 7
,7 8
IMapper9 @

automapperA K
)K L
:M N
IRoleServiceO [
{ 
public 

List 
< 
string 
> 
GetAllPermissions )
() *
)* +
{ 
return 
[ 
.. 
Permissions 
. 
PermissionList -
.- .
Values. 4
]4 5
;5 6
} 
public 

List 
< 
	RoleModel 
> 
GetAllRoles &
(& '
RoleManager' 2
<2 3
AppRole3 :
>: ;
roleManager< G
)G H
{ 
var 
roles 
= 
roleManager 
.  
Roles  %
.% &
ToList& ,
(, -
)- .
;. /
return 

automapper 
. 
Map 
< 
List "
<" #
	RoleModel# ,
>, -
>- .
(. /
roles/ 4
)4 5
;5 6
} 
public 

async 
Task 
< 
	RoleModel 
>  
GetRoleByIdAsync! 1
(1 2
RoleManager2 =
<= >
AppRole> E
>E F
roleManagerG R
,R S
GuidT X
roleIdY _
)_ `
{ 
var 
role 
= 
await 
roleManager $
.$ %
FindByIdAsync% 2
(2 3
roleId3 9
.9 :
ToString: B
(B C
)C D
)D E
;E F
return 
role 
== 
null 
? 
throw #
new$ '
GamestoreException( :
(: ;
$"; =
$str= J
{J K
roleIdK Q
}Q R
$strR b
"b c
)c d
:e f

automapperg q
.q r
Mapr u
<u v
	RoleModelv 
>	 �
(
� �
role
� �
)
� �
;
� �
}   
public"" 

async"" 
Task"" 
<"" 
List"" 
<"" 
string"" !
>""! "
>""" #'
GetPermissionsByRoleIdAsync""$ ?
(""? @
RoleManager""@ K
<""K L
AppRole""L S
>""S T
roleManager""U `
,""` a
Guid""b f
roleId""g m
)""m n
{## 
var$$ 
role$$ 
=$$ 
await$$ 
roleManager$$ $
.$$$ %
FindByIdAsync$$% 2
($$2 3
roleId$$3 9
.$$9 :
ToString$$: B
($$B C
)$$C D
)$$D E
;$$E F
if%% 

(%% 
role%% 
==%% 
null%% 
)%% 
{&& 	
throw'' 
new'' 
GamestoreException'' (
(''( )
$"'') +
$str''+ 8
{''8 9
roleId''9 ?
}''? @
$str''@ P
"''P Q
)''Q R
;''R S
}(( 	
var** 
permissions** 
=** 

unitOfWork** $
.**$ %
RoleClaimRepository**% 8
.**8 9"
GetClaimsByRoleIdAsync**9 O
(**O P
roleId**P V
)**V W
.**W X
ToList**X ^
(**^ _
)**_ `
;**` a
var++ 
permissionList++ 
=++  
CreatePermissionList++ 1
(++1 2
permissions++2 =
)++= >
;++> ?
return-- 
permissionList-- 
;-- 
}.. 
public00 

async00 
Task00 
<00 
IdentityResult00 $
>00$ %
AddRoleAsync00& 2
(002 3
RoleManager003 >
<00> ?
AppRole00? F
>00F G
roleManager00H S
,00S T
RoleModelDto00U a
role00b f
)00f g
{11 
if22 

(22 
await22 
roleManager22 
.22 
RoleExistsAsync22 -
(22- .
role22. 2
.222 3
Role223 7
.227 8
Name228 <
)22< =
)22= >
{33 	
return44 
IdentityResult44 !
.44! "
Failed44" (
(44( )
new44) ,
IdentityError44- :
{44; <
Description44= H
=44I J
$"44K M
$str44M R
{44R S
role44S W
.44W X
Role44X \
.44\ ]
Name44] a
}44a b
$str44b r
"44r s
}44t u
)44u v
;44v w
}55 	
var77 
identityRole77 
=77 
new77 
AppRole77 &
(77& '
)77' (
{77) *
Name77+ /
=770 1
role772 6
.776 7
Role777 ;
.77; <
Name77< @
}77A B
;77B C
var88 
result88 
=88 
await88 
roleManager88 &
.88& '
CreateAsync88' 2
(882 3
identityRole883 ?
)88? @
;88@ A
await99 *
AddRoleClaimsToRepositoryAsync99 ,
(99, -

unitOfWork99- 7
,997 8
role999 =
.99= >
Permissions99> I
!99I J
,99J K
identityRole99L X
)99X Y
;99Y Z
await:: 

unitOfWork:: 
.:: 
	SaveAsync:: "
(::" #
)::# $
;::$ %
return<< 
result<< 
;<< 
}== 
public?? 

async?? 
Task?? 
<?? 
IdentityResult?? $
>??$ %
UpdateRoleAsync??& 5
(??5 6
RoleManager??6 A
<??A B
AppRole??B I
>??I J
roleManager??K V
,??V W
RoleModelDto??X d
roleDto??e l
)??l m
{@@ 
varAA 
identityRoleAA 
=AA 
awaitAA  
roleManagerAA! ,
.AA, -
FindByIdAsyncAA- :
(AA: ;
roleDtoAA; B
.AAB C
RoleAAC G
.AAG H
IdAAH J
.AAJ K
ToStringAAK S
(AAS T
)AAT U
!AAU V
)AAV W
;AAW X
ifBB 

(BB 
identityRoleBB 
==BB 
nullBB  
)BB  !
{CC 	
returnDD 
IdentityResultDD !
.DD! "
FailedDD" (
(DD( )
newDD) ,
IdentityErrorDD- :
{DD; <
DescriptionDD= H
=DDI J
$"DDK M
$strDDM R
{DDR S
roleDtoDDS Z
.DDZ [
RoleDD[ _
.DD_ `
NameDD` d
}DDd e
$strDDe t
"DDt u
}DDv w
)DDw x
;DDx y
}EE 	
identityRoleGG 
.GG 
NameGG 
=GG 
roleDtoGG #
.GG# $
RoleGG$ (
.GG( )
NameGG) -
;GG- .
identityRoleHH 
.HH 
NormalizedNameHH #
=HH$ %
roleDtoHH& -
.HH- .
RoleHH. 2
.HH2 3
NameHH3 7
.HH7 8
ToUpperHH8 ?
(HH? @
CultureInfoHH@ K
.HHK L
InvariantCultureHHL \
)HH\ ]
;HH] ^

unitOfWorkII 
.II 
RoleRepositoryII !
.II! "
UpdateII" (
(II( )
identityRoleII) 5
)II5 6
;II6 7
awaitJJ -
!UpdateRoleCialmsInrepositoryAsyncJJ /
(JJ/ 0

unitOfWorkJJ0 :
,JJ: ;
roleDtoJJ< C
,JJC D
identityRoleJJE Q
)JJQ R
;JJR S
awaitKK 

unitOfWorkKK 
.KK 
	SaveAsyncKK "
(KK" #
)KK# $
;KK$ %
returnMM 
IdentityResultMM 
.MM 
SuccessMM %
;MM% &
}NN 
publicPP 

asyncPP 
TaskPP 
<PP 
IdentityResultPP $
>PP$ %
DeleteRoleByIdAsyncPP& 9
(PP9 :
RoleManagerPP: E
<PPE F
AppRolePPF M
>PPM N
roleManagerPPO Z
,PPZ [
GuidPP\ `
roleIdPPa g
)PPg h
{QQ 
varRR 
roleRR 
=RR 
awaitRR 
roleManagerRR $
.RR$ %
FindByIdAsyncRR% 2
(RR2 3
roleIdRR3 9
.RR9 :
ToStringRR: B
(RRB C
)RRC D
)RRD E
;RRE F
ifSS 

(SS 
roleSS 
==SS 
nullSS 
)SS 
{TT 	
returnUU 
IdentityResultUU !
.UU! "
FailedUU" (
(UU( )
newUU) ,
IdentityErrorUU- :
{UU; <
DescriptionUU= H
=UUI J
$"UUK M
$strUUM Z
{UUZ [
roleIdUU[ a
}UUa b
$strUUb r
"UUr s
}UUt u
)UUu v
;UUv w
}VV 	
varXX 
resultXX 
=XX 
awaitXX 
roleManagerXX &
.XX& '
DeleteAsyncXX' 2
(XX2 3
roleXX3 7
)XX7 8
;XX8 9
returnZZ 
resultZZ 
;ZZ 
}[[ 
private]] 
static]] 
async]] 
Task]] *
AddRoleClaimsToRepositoryAsync]] <
(]]< =
IIdentityUnitOfWork]]= P

unitOfWork]]Q [
,]][ \
List]]] a
<]]a b
string]]b h
>]]h i
permissions]]j u
,]]u v
AppRole]]w ~
identityRole	]] �
)
]]� �
{^^ 
foreach__ 
(__ 
var__ 
item__ 
in__ 
permissions__ (
)__( )
{`` 	
varaa 
	roleClaimaa 
=aa 
newaa 
	RoleClaimaa  )
(aa) *
)aa* +
{bb 
RoleIdcc 
=cc 
identityRolecc %
.cc% &
Idcc& (
,cc( )
	ClaimTypedd 
=dd 
itemdd  
,dd  !

ClaimValueee 
=ee 
itemee !
,ee! "
}ff 
;ff 
awaithh 

unitOfWorkhh 
.hh 
RoleClaimRepositoryhh 0
.hh0 1
AddAsynchh1 9
(hh9 :
	roleClaimhh: C
)hhC D
;hhD E
}ii 	
}jj 
privatell 
staticll 
asyncll 
Taskll -
!UpdateRoleCialmsInrepositoryAsyncll ?
(ll? @
IIdentityUnitOfWorkll@ S

unitOfWorkllT ^
,ll^ _
RoleModelDtoll` l
roleDtollm t
,llt u
AppRolellv }
?ll} ~
identityRole	ll �
)
ll� �
{mm 

unitOfWorknn 
.nn 
RoleClaimRepositorynn &
.nn& ' 
DeleteClaimsByRoleIdnn' ;
(nn; <
identityRolenn< H
.nnH I
IdnnI K
)nnK L
;nnL M
awaitoo *
AddRoleClaimsToRepositoryAsyncoo ,
(oo, -

unitOfWorkoo- 7
,oo7 8
roleDtooo9 @
.oo@ A
PermissionsooA L
!ooL M
,ooM N
identityRoleooO [
)oo[ \
;oo\ ]
}pp 
privaterr 
staticrr 
Listrr 
<rr 
stringrr 
>rr  
CreatePermissionListrr  4
(rr4 5
Listrr5 9
<rr9 :
IdentityRoleClaimrr: K
<rrK L
stringrrL R
>rrR S
>rrS T
permissionsrrU `
)rr` a
{ss 
Listtt 
<tt 
stringtt 
>tt 
permissionListtt #
=tt$ %
[tt& '
]tt' (
;tt( )
foreachuu 
(uu 
varuu 

permissionuu 
inuu  "
permissionsuu# .
)uu. /
{vv 	
permissionListww 
.ww 
Addww 
(ww 

permissionww )
.ww) *

ClaimValueww* 4
!ww4 5
)ww5 6
;ww6 7
}xx 	
returnzz 
permissionListzz 
;zz 
}{{ 
}|| �
fD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\ShipperService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
public

 
class

 
ShipperService

 
(

 
IMongoUnitOfWork

 ,
mongoUnitOfWork

- <
,

< =
IMapper

> E

automapper

F P
,

P Q
ILogger

R Y
<

Y Z
GameService

Z e
>

e f
logger

g m
)

m n
:

o p
IShipperService	

q �
{ 
public 

async 
Task 
< 
List 
< 
ShipperModelDto *
>* +
>+ ,
GetAllShippersAsync- @
(@ A
)A B
{ 
logger 
. 
LogInformation 
( 
$str 4
)4 5
;5 6
var 
shippers 
= 
await 
mongoUnitOfWork ,
., -
ShipperRepository- >
.> ?
GetAllAsync? J
(J K
)K L
;L M
return 

automapper 
. 
Map 
< 
List "
<" #
ShipperModelDto# 2
>2 3
>3 4
(4 5
shippers5 =
)= >
;> ?
} 
} Ѷ
nD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\SqlServerHelperService.cs
	namespace

 	
	Gamestore


 
.

 
BLL

 
.

 
Services

  
;

  !
internal 
static	 
class "
SqlServerHelperService ,
{ 
internal 
static 
async 
Task )
FilterGamesFromSQLServerAsync <
(< =
IUnitOfWork= H

unitOfWorkI S
,S T
IMongoUnitOfWorkU e
mongoUnitOfWorkf u
,u v
IMapperw ~

automapper	 �
,
� �
GameFiltersDto
� �
gameFilters
� �
,
� �
FilteredGamesDto
� �
filteredGameDtos
� �
,
� �,
IGameProcessingPipelineService
� �+
gameProcessingPipelineService
� �
,
� �
bool
� � 
canSeeDeletedGames
� �
)
� �
{ 

IQueryable 
< 
Game 
> 
games 
; 
if 

( 
canSeeDeletedGames 
) 
{ 	
games 
= 

unitOfWork 
. 
GameRepository -
.- .*
GetGamesWithDeletedAsQueryable. L
(L M
)M N
;N O
} 	
else 
{ 	
games 
= 

unitOfWork 
. 
GameRepository -
.- .
GetGamesAsQueryable. A
(A B
)B C
;C D
} 	
var 
gamesFromSQLServer 
=  
(! "
await" ')
gameProcessingPipelineService( E
.E F
ProcessGamesAsyncF W
(W X

unitOfWorkX b
,b c
mongoUnitOfWorkd s
,s t
gameFilters	u �
,
� �
games
� �
)
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
if 

( 
gamesFromSQLServer 
. 
Count $
!=% '
$num( )
)) *
{ 	
filteredGameDtos 
. 
Games "
." #
AddRange# +
(+ ,

automapper, 6
.6 7
Map7 :
<: ;
List; ?
<? @
GameModelDto@ L
>L M
>M N
(N O
gamesFromSQLServerO a
)a b
)b c
;c d
} 	
} 
internal!! 
static!! 
async!! 
Task!! $
UpdateExistingOrderAsync!! 7
(!!7 8
IUnitOfWork!!8 C

unitOfWork!!D N
,!!N O
IMapper!!P W

automapper!!X b
,!!b c
int!!d g
quantity!!h p
,!!p q
GameModelDto!!r ~
game	!! �
,
!!� �
int
!!� �
unitInStock
!!� �
,
!!� �
Order
!!� �
?
!!� �
exisitngOrder
!!� �
)
!!� �
{"" 
if## 

(## 
game## 
.## 
Id## 
is## 
not## 
null## 
)##  
{$$ 	
	OrderGame%% 
existingOrderGame%% '
=%%( )
await%%* /

unitOfWork%%0 :
.%%: ;
OrderGameRepository%%; N
.%%N O)
GetByOrderIdAndProductIdAsync%%O l
(%%l m
exisitngOrder%%m z
.%%z {
Id%%{ }
,%%} ~
(	%% �
Guid
%%� �
)
%%� �
game
%%� �
.
%%� �
Id
%%� �
)
%%� �
;
%%� �
if'' 
('' 
existingOrderGame'' !
!=''" $
null''% )
)'') *
{(( 
await)) (
UpdateExistingOrderGameAsync)) 2
())2 3

unitOfWork))3 =
,))= >
quantity))? G
,))G H
unitInStock))I T
,))T U
existingOrderGame))V g
)))g h
;))h i
}** 
else++ 
{,, 
await-- #
CreateNewOrderGameAsync-- -
(--- .

unitOfWork--. 8
,--8 9

automapper--: D
,--D E
quantity--F N
,--N O
game--P T
,--T U
unitInStock--V a
,--a b
exisitngOrder--c p
)--p q
;--q r
}.. 
}// 	
}00 
internal22 
static22 
async22 
Task22 A
5CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync22 T
(22T U
IUnitOfWork22U `

unitOfWork22a k
,22k l
IMapper22m t

automapper22u 
,	22 �
GameModelDto
22� �
game
22� �
,
22� �
Game
22� �
?
22� �
gameInSQLServer
22� �
)
22� �
{33 
if44 

(44 
gameInSQLServer44 
is44 
null44 #
)44# $
{55 	
var66 
	gameToAdd66 
=66 

automapper66 &
.66& '
Map66' *
<66* +
Game66+ /
>66/ 0
(660 1
game661 5
)665 6
;666 7
await88 4
(CreateGenreInSQLServerIfDoesntExistAsync88 :
(88: ;

unitOfWork88; E
,88E F
game88G K
,88K L
	gameToAdd88M V
)88V W
;88W X
await99 8
,CreatePublisherInSQLServerIfDoesntExistAsync99 >
(99> ?

unitOfWork99? I
,99I J
game99K O
,99O P
	gameToAdd99Q Z
)99Z [
;99[ \
await:: 

unitOfWork:: 
.:: 
GameRepository:: +
.::+ ,
AddAsync::, 4
(::4 5
	gameToAdd::5 >
)::> ?
;::? @
await;; 

unitOfWork;; 
.;; 
	SaveAsync;; &
(;;& '
);;' (
;;;( )
}<< 	
}== 
internal?? 
static?? 
async?? 
Task?? 
<?? 
List?? #
<??# $
GenreModelDto??$ 1
>??1 2
>??2 30
$GetGenresFromSQLServerByGameKeyAsync??4 X
(??X Y
IUnitOfWork??Y d

unitOfWork??e o
,??o p
IMapper??q x

automapper	??y �
,
??� �
string
??� �
gameKey
??� �
)
??� �
{@@ 
varAA 
gameAA 
=AA 
awaitAA 

unitOfWorkAA #
.AA# $
GameRepositoryAA$ 2
.AA2 3
GetGameByKeyAsyncAA3 D
(AAD E
gameKeyAAE L
)AAL M
;AAM N
ListCC 
<CC 
GenreModelDtoCC 
>CC 
genreModelsCC '
=CC( )
[CC* +
]CC+ ,
;CC, -
ifDD 

(DD 
gameDD 
isDD 
notDD 
nullDD 
)DD 
{EE 	
varFF 
genresFF 
=FF 
awaitFF 

unitOfWorkFF )
.FF) *
GameRepositoryFF* 8
.FF8 9 
GetGenresByGameAsyncFF9 M
(FFM N
gameFFN R
.FFR S
IdFFS U
)FFU V
;FFV W
foreachHH 
(HH 
varHH 
genreHH 
inHH !
genresHH" (
)HH( )
{II 
genreModelsJJ 
.JJ 
AddJJ 
(JJ  

automapperJJ  *
.JJ* +
MapJJ+ .
<JJ. /
GenreModelDtoJJ/ <
>JJ< =
(JJ= >
genreJJ> C
)JJC D
)JJD E
;JJE F
}KK 
returnMM 
genreModelsMM 
;MM 
}NN 	
returnPP 
nullPP 
;PP 
}QQ 
internalSS 
staticSS 
asyncSS 
TaskSS 
<SS 
PublisherModelDtoSS 0
>SS0 13
'GetPublisherFromSQLServerByGameKeyAsyncSS2 Y
(SSY Z
IUnitOfWorkSSZ e

unitOfWorkSSf p
,SSp q
IMapperSSr y

automapper	SSz �
,
SS� �
string
SS� �
gameKey
SS� �
)
SS� �
{TT 
varUU 
gameUU 
=UU 
awaitUU 

unitOfWorkUU #
.UU# $
GameRepositoryUU$ 2
.UU2 3
GetGameByKeyAsyncUU3 D
(UUD E
gameKeyUUE L
)UUL M
;UUM N
ifVV 

(VV 
gameVV 
isVV 
notVV 
nullVV 
)VV 
{WW 	
varXX 
	publisherXX 
=XX 
awaitXX !

unitOfWorkXX" ,
.XX, -
GameRepositoryXX- ;
.XX; <#
GetPublisherByGameAsyncXX< S
(XXS T
gameXXT X
.XXX Y
IdXXY [
)XX[ \
;XX\ ]
returnZZ 

automapperZZ 
.ZZ 
MapZZ !
<ZZ! "
PublisherModelDtoZZ" 3
>ZZ3 4
(ZZ4 5
	publisherZZ5 >
)ZZ> ?
;ZZ? @
}[[ 	
return]] 
null]] 
;]] 
}^^ 
internal`` 
static`` 
async`` 
Task`` 
<`` 
GameModelDto`` +
>``+ ,*
GetGameFromSQLServerByKeyAsync``- K
(``K L
IUnitOfWork``L W

unitOfWork``X b
,``b c
IMapper``d k

automapper``l v
,``v w
string``x ~
key	`` �
)
``� �
{aa 
varbb 
gamebb 
=bb 
awaitbb 

unitOfWorkbb #
.bb# $
GameRepositorybb$ 2
.bb2 3
GetGameByKeyAsyncbb3 D
(bbD E
keybbE H
)bbH I
;bbI J
awaitcc (
IncreaseGameViewCounterAsynccc *
(cc* +

unitOfWorkcc+ 5
,cc5 6
gamecc7 ;
)cc; <
;cc< =
returndd 

automapperdd 
.dd 
Mapdd 
<dd 
GameModelDtodd *
>dd* +
(dd+ ,
gamedd, 0
)dd0 1
;dd1 2
}ee 
internalgg 
staticgg 
asyncgg 
Taskgg 
<gg 
Gamegg #
?gg# $
>gg$ %)
GetGameFromSQLServerByIdAsyncgg& C
(ggC D
IUnitOfWorkggD O

unitOfWorkggP Z
,ggZ [
Guidgg\ `
gameIdgga g
)ggg h
{hh 
returnii 
awaitii 

unitOfWorkii 
.ii  
GameRepositoryii  .
.ii. /
GetByIdAsyncii/ ;
(ii; <
gameIdii< B
)iiB C
;iiC D
}jj 
privatell 
staticll 
asyncll 
Taskll (
IncreaseGameViewCounterAsyncll :
(ll: ;
IUnitOfWorkll; F

unitOfWorkllG Q
,llQ R
GamellS W
?llW X
gamellY ]
)ll] ^
{mm 
ifnn 

(nn 
gamenn 
isnn 
notnn 
nullnn 
)nn 
{oo 	
gamepp 
.pp 
NumberOfViewspp 
++pp  
;pp  !
awaitqq 

unitOfWorkqq 
.qq 
	SaveAsyncqq &
(qq& '
)qq' (
;qq( )
}rr 	
}ss 
privateuu 
staticuu 
asyncuu 
Taskuu 4
(CreateGenreInSQLServerIfDoesntExistAsyncuu F
(uuF G
IUnitOfWorkuuG R

unitOfWorkuuS ]
,uu] ^
GameModelDtouu_ k
gameuul p
,uup q
Gameuur v
	gameToAdd	uuw �
)
uu� �
{vv 
varww 

firstGenreww 
=ww 
gameww 
.ww 
Genresww $
[ww$ %
$numww% &
]ww& '
;ww' (
ifxx 

(xx 

firstGenrexx 
!=xx 
nullxx 
&&xx !

firstGenrexx" ,
.xx, -
Idxx- /
!=xx0 2
nullxx3 7
&&xx8 :
gamexx; ?
.xx? @
Idxx@ B
!=xxC E
nullxxF J
)xxJ K
{yy 	
varzz 
existingGenrezz 
=zz 
awaitzz  %

unitOfWorkzz& 0
.zz0 1
GenreRepositoryzz1 @
.zz@ A
GetByIdAsynczzA M
(zzM N
(zzN O
GuidzzO S
)zzS T

firstGenrezzT ^
.zz^ _
Idzz_ a
)zza b
;zzb c
if{{ 
({{ 
existingGenre{{ 
is{{  
null{{! %
){{% &
{|| 
await}} 

unitOfWork}}  
.}}  !
GenreRepository}}! 0
.}}0 1
AddAsync}}1 9
(}}9 :
new}}: =
(}}= >
)}}> ?
{}}@ A
Id}}B D
=}}E F
(}}G H
Guid}}H L
)}}L M

firstGenre}}M W
.}}W X
Id}}X Z
,}}Z [
Name}}\ `
=}}a b

firstGenre}}c m
.}}m n
Name}}n r
}}}s t
)}}t u
;}}u v
}~~ 
	gameToAdd
�� 
.
�� 
ProductCategories
�� '
=
��( )
[
�� 
new
�� 

GameGenres
�� 
{
��  !
GameId
��" (
=
��) *
game
��+ /
.
��/ 0
Id
��0 2
.
��2 3
Value
��3 8
,
��8 9
GenreId
��: A
=
��B C
(
��D E
Guid
��E I
)
��I J

firstGenre
��J T
.
��T U
Id
��U W
}
��X Y
]
�� 
;
�� 
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� :
,CreatePublisherInSQLServerIfDoesntExistAsync
�� J
(
��J K
IUnitOfWork
��K V

unitOfWork
��W a
,
��a b
GameModelDto
��c o
game
��p t
,
��t u
Game
��v z
?
��z {
gameInSQLServer��| �
)��� �
{
�� 
if
�� 

(
�� 
game
�� 
.
�� 
	Publisher
�� 
is
�� 
not
�� !
null
��" &
&&
��' )
game
��* .
.
��. /
	Publisher
��/ 8
.
��8 9
Id
��9 ;
is
��< >
not
��? B
null
��C G
)
��G H
{
�� 	
var
�� "
publisherInSQLServer
�� $
=
��% &
await
��' ,

unitOfWork
��- 7
.
��7 8!
PublisherRepository
��8 K
.
��K L
GetByIdAsync
��L X
(
��X Y
(
��Y Z
Guid
��Z ^
)
��^ _
game
��_ c
.
��c d
	Publisher
��d m
.
��m n
Id
��n p
)
��p q
;
��q r
if
�� 
(
�� "
publisherInSQLServer
�� $
is
��% '
null
��( ,
)
��, -
{
�� 
await
�� 

unitOfWork
��  
.
��  !!
PublisherRepository
��! 4
.
��4 5
AddAsync
��5 =
(
��= >
new
��> A
	Publisher
��B K
{
��L M
Id
��N P
=
��Q R
(
��S T
Guid
��T X
)
��X Y
game
��Y ]
.
��] ^
	Publisher
��^ g
.
��g h
Id
��h j
,
��j k
CompanyName
��l w
=
��x y
game
��z ~
.
��~ 
	Publisher�� �
.��� �
CompanyName��� �
}��� �
)��� �
;��� �
await
�� 

unitOfWork
��  
.
��  !
	SaveAsync
��! *
(
��* +
)
��+ ,
;
��, -
await
�� /
!AttachPublisherFromSQLServerAsync
�� 7
(
��7 8

unitOfWork
��8 B
,
��B C
game
��D H
,
��H I
gameInSQLServer
��J Y
)
��Y Z
;
��Z [
}
�� 
else
�� 
{
�� 
var
�� 
publisherId
�� 
=
��  !
game
��" &
.
��& '
	Publisher
��' 0
.
��0 1
Id
��1 3
;
��3 4
if
�� 
(
�� 
publisherId
�� 
is
��  "
not
��# &
null
��' +
)
��+ ,
{
�� 
var
�� 
pub
�� 
=
�� 
await
�� #

unitOfWork
��$ .
.
��. /!
PublisherRepository
��/ B
.
��B C
GetByIdAsync
��C O
(
��O P
(
��P Q
Guid
��Q U
)
��U V
publisherId
��V a
)
��a b
;
��b c
if
�� 
(
�� 
pub
�� 
is
�� 
not
�� "
null
��# '
)
��' (
{
�� 
await
�� /
!AttachPublisherFromSQLServerAsync
�� ?
(
��? @

unitOfWork
��@ J
,
��J K
game
��L P
,
��P Q
gameInSQLServer
��R a
)
��a b
;
��b c
}
�� 
}
�� 
}
�� 
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� /
!AttachPublisherFromSQLServerAsync
�� ?
(
��? @
IUnitOfWork
��@ K

unitOfWork
��L V
,
��V W
GameModelDto
��X d
game
��e i
,
��i j
Game
��k o
?
��o p
gameInSQLServer��q �
)��� �
{
�� 
var
�� 
publisherId
�� 
=
�� 
game
�� 
.
�� 
	Publisher
�� (
.
��( )
Id
��) +
;
��+ ,
if
�� 

(
�� 
publisherId
�� 
is
�� 
not
�� 
null
�� #
)
��# $
{
�� 	
var
�� 
pub
�� 
=
�� 
await
�� 

unitOfWork
�� &
.
��& '!
PublisherRepository
��' :
.
��: ;
GetByIdAsync
��; G
(
��G H
(
��H I
Guid
��I M
)
��M N
publisherId
��N Y
)
��Y Z
;
��Z [
if
�� 
(
�� 
pub
�� 
is
�� 
not
�� 
null
�� 
)
��  
{
�� 
gameInSQLServer
�� 
.
��  
	Publisher
��  )
=
��* +
pub
��, /
;
��/ 0
}
�� 
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� *
UpdateExistingOrderGameAsync
�� :
(
��: ;
IUnitOfWork
��; F

unitOfWork
��G Q
,
��Q R
int
��S V
quantity
��W _
,
��_ `
int
��a d
unitInStock
��e p
,
��p q
	OrderGame
��r { 
existingOrderGame��| �
)��� �
{
�� 
var
�� #
expectedTotalQuantity
�� !
=
��" #
quantity
��$ ,
+
��- .
existingOrderGame
��/ @
.
��@ A
Quantity
��A I
;
��I J#
expectedTotalQuantity
�� 
=
�� #
expectedTotalQuantity
��  5
<
��6 7
unitInStock
��8 C
?
��D E#
expectedTotalQuantity
��F [
:
��\ ]
unitInStock
��^ i
;
��i j
existingOrderGame
�� 
.
�� 
Quantity
�� "
=
��# $#
expectedTotalQuantity
��% :
;
��: ;
await
�� 

unitOfWork
�� 
.
�� !
OrderGameRepository
�� ,
.
��, -
UpdateAsync
��- 8
(
��8 9
existingOrderGame
��9 J
)
��J K
;
��K L
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� "
(
��" #
)
��# $
;
��$ %
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� %
CreateNewOrderGameAsync
�� 5
(
��5 6
IUnitOfWork
��6 A

unitOfWork
��B L
,
��L M
IMapper
��N U

automapper
��V `
,
��` a
int
��b e
quantity
��f n
,
��n o
GameModelDto
��p |
game��} �
,��� �
int��� �
unitInStock��� �
,��� �
Order��� �
?��� �
exisitngOrder��� �
)��� �
{
�� 
if
�� 

(
�� 
game
�� 
.
�� 
Id
�� 
is
�� 
not
�� 
null
�� 
)
��  
{
�� 	
var
�� #
expectedTotalQuantity
�� %
=
��& '
quantity
��( 0
<
��1 2
unitInStock
��3 >
?
��? @
quantity
��A I
:
��J K
unitInStock
��L W
;
��W X
var
�� 
gameInSQLServer
�� 
=
��  !
await
��" '

unitOfWork
��( 2
.
��2 3
GameRepository
��3 A
.
��A B
GetByIdAsync
��B N
(
��N O
(
��O P
Guid
��P T
)
��T U
game
��U Y
.
��Y Z
Id
��Z \
)
��\ ]
;
��] ^
if
�� 
(
�� 
gameInSQLServer
�� 
is
��  "
null
��# '
)
��' (
{
�� 
await
�� C
5CopyGameFromMongoDBToSQLServerIfDoesntExistThereAsync
�� K
(
��K L

unitOfWork
��L V
,
��V W

automapper
��X b
,
��b c
game
��d h
,
��h i
gameInSQLServer
��j y
)
��y z
;
��z {
}
�� 
	OrderGame
�� 
newOrderGame
�� "
=
��# $
new
��% (
	OrderGame
��) 2
(
��2 3
)
��3 4
{
�� 
OrderId
�� 
=
�� 
exisitngOrder
�� '
.
��' (
Id
��( *
,
��* +
GameId
�� 
=
�� 
game
�� 
.
�� 
Id
��  
??
��! #
Guid
��$ (
.
��( )
Empty
��) .
,
��. /
Price
�� 
=
�� 
game
�� 
.
�� 
Price
�� "
,
��" #
Discount
�� 
=
�� 
game
�� 
.
��  
Discontinued
��  ,
,
��, -
Quantity
�� 
=
�� #
expectedTotalQuantity
�� 0
,
��0 1
}
�� 
;
�� 
await
�� 

unitOfWork
�� 
.
�� !
OrderGameRepository
�� 0
.
��0 1
AddAsync
��1 9
(
��9 :
newOrderGame
��: F
)
��F G
;
��G H
await
�� 

unitOfWork
�� 
.
�� 
	SaveAsync
�� &
(
��& '
)
��' (
;
��( )
}
�� 	
}
�� 
}�� ��
cD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Services\UserService.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 
Services  
;  !
public 
class 
UserService 
( 
IMapper  

automapper! +
)+ ,
:- .
IUserService/ ;
{ 
private 
const 
string 
	EmailStub "
=# $
$str% :
;: ;
public 

List 
< 
CustomerDto 
> 
GetAllUsers (
(( )
UserManager) 4
<4 5
AppUser5 <
>< =
userManager> I
)I J
{ 
var 
users 
= 
userManager 
.  
Users  %
.% &
ToList& ,
(, -
)- .
;. /
return 

automapper 
. 
Map 
< 
List "
<" #
CustomerDto# .
>. /
>/ 0
(0 1
users1 6
)6 7
;7 8
} 
public 

async 
Task 
< 
CustomerDto !
>! "
GetUserByIdAsync# 3
(3 4
UserManager4 ?
<? @
AppUser@ G
>G H
userManagerI T
,T U
stringV \
userId] c
)c d
{ 
var 
user 
= 
await 
userManager $
.$ %
Users% *
.* +
FirstOrDefaultAsync+ >
(> ?
x? @
=>A C
xD E
.E F
IdF H
==I K
userIdL R
)R S
;S T
return 

automapper 
. 
Map 
< 
CustomerDto )
>) *
(* +
user+ /
)/ 0
;0 1
} 
public!! 

async!! 
Task!! 
<!! 
List!! 
<!! 
UserRoleDto!! &
>!!& '
>!!' ( 
GetUserRolesByUserId!!) =
(!!= >
UserManager!!> I
<!!I J
AppUser!!J Q
>!!Q R
userManager!!S ^
,!!^ _
RoleManager!!` k
<!!k l
AppRole!!l s
>!!s t
roleManager	!!u �
,
!!� �
string
!!� �
userId
!!� �
)
!!� �
{"" 
var## 
user## 
=## 
await## 
userManager## $
.##$ %
Users##% *
.##* +
FirstOrDefaultAsync##+ >
(##> ?
x##? @
=>##A C
x##D E
.##E F
Id##F H
==##I K
userId##L R
)##R S
;##S T
if$$ 

($$ 
user$$ 
==$$ 
null$$ 
)$$ 
{%% 	
throw&& 
new&& 
GamestoreException&& (
(&&( )
$str&&) :
)&&: ;
;&&; <
}'' 	
List)) 
<)) 
UserRoleDto)) 
>)) 
	userRoles)) #
=))$ %
[))& '
]))' (
;))( )
await**  
FindRolesByUserAsync** "
(**" #
userManager**# .
,**. /
roleManager**0 ;
,**; <
user**= A
,**A B
	userRoles**C L
)**L M
;**M N
return,, 
	userRoles,, 
;,, 
}-- 
public// 

async// 
Task// 
<// 
string// 
>// 

LoginAsync// (
(//( )
UserManager//) 4
<//4 5
AppUser//5 <
>//< =
userManager//> I
,//I J
RoleManager//K V
<//V W
AppRole//W ^
>//^ _
roleManager//` k
,//k l
IConfiguration//m {
configuration	//| �
,
//� �
LoginModelDto
//� �
login
//� �
)
//� �
{00 
var11 
user11 
=11 
await11 
userManager11 $
.11$ %
FindByNameAsync11% 4
(114 5
login115 :
.11: ;
Model11; @
.11@ A
Login11A F
)11F G
;11G H
if22 

(22 
user22 
==22 
null22 
||22 
!22 
await22 "
userManager22# .
.22. /
CheckPasswordAsync22/ A
(22A B
user22B F
,22F G
login22H M
.22M N
Model22N S
.22S T
Password22T \
)22\ ]
)22] ^
{33 	
throw44 
new44 
GamestoreException44 (
(44( )
$str44) :
)44: ;
;44; <
}55 	
var77 
generatedToken77 
=77 
await77 "

JwtHelpers77# -
.77- .
GenerateJwtToken77. >
(77> ?
userManager77? J
,77J K
roleManager77L W
,77W X
configuration77Y f
,77f g
user77h l
)77l m
;77m n
string88 
token88 
=88 
$"88 
$str88  
{88  !
generatedToken88! /
}88/ 0
"880 1
;881 2
return:: 
token:: 
;:: 
};; 
public== 

async== 
Task== 
<== 
IdentityResult== $
>==$ %
UpdateUserAsync==& 5
(==5 6
UserManager==6 A
<==A B
AppUser==B I
>==I J
userManager==K V
,==V W
RoleManager==X c
<==c d
AppRole==d k
>==k l
roleManager==m x
,==x y
UserDto	==z �
user
==� �
)
==� �
{>> 
var?? 
userToUpdate?? 
=?? 
await??  
userManager??! ,
.??, -
FindByIdAsync??- :
(??: ;
user??; ?
.??? @
User??@ D
.??D E
Id??E G
)??G H
;??H I
if@@ 

(@@ 
userToUpdate@@ 
==@@ 
null@@  
)@@  !
{AA 	
throwBB 
newBB 
GamestoreExceptionBB (
(BB( )
$strBB) :
)BB: ;
;BB; <
}CC 	
UpdateUserDetailsEE 
(EE 
userEE 
,EE 
userToUpdateEE  ,
)EE, -
;EE- .
awaitFF #
UpdateUserPasswordAsyncFF %
(FF% &
userManagerFF& 1
,FF1 2
userFF3 7
,FF7 8
userToUpdateFF9 E
)FFE F
;FFF G
awaitGG  
UpdateUserRolesAsyncGG "
(GG" #
userManagerGG# .
,GG. /
roleManagerGG0 ;
,GG; <
userGG= A
,GGA B
userToUpdateGGC O
)GGO P
;GGP Q
returnII 
awaitII 
userManagerII  
.II  !
UpdateAsyncII! ,
(II, -
userToUpdateII- 9
)II9 :
;II: ;
}JJ 
publicLL 

asyncLL 
TaskLL 
DeleteUserAsyncLL %
(LL% &
UserManagerLL& 1
<LL1 2
AppUserLL2 9
>LL9 :
userManagerLL; F
,LLF G
stringLLH N
userIdLLO U
)LLU V
{MM 
varNN 
userNN 
=NN 
awaitNN 
userManagerNN $
.NN$ %
UsersNN% *
.NN* +
FirstOrDefaultAsyncNN+ >
(NN> ?
xNN? @
=>NNA C
xNND E
.NNE F
IdNNF H
==NNI K
userIdNNL R
)NNR S
;NNS T
ifOO 

(OO 
userOO 
==OO 
nullOO 
)OO 
{PP 	
throwQQ 
newQQ 
GamestoreExceptionQQ (
(QQ( )
$strQQ) 9
)QQ9 :
;QQ: ;
}RR 	
awaitTT 
userManagerTT 
.TT 
DeleteAsyncTT %
(TT% &
userTT& *
)TT* +
;TT+ ,
}UU 
publicWW 

asyncWW 
TaskWW 
AddUserAsyncWW "
(WW" #
UserManagerWW# .
<WW. /
AppUserWW/ 6
>WW6 7
userManagerWW8 C
,WWC D
RoleManagerWWE P
<WWP Q
AppRoleWWQ X
>WWX Y
roleManagerWWZ e
,WWe f
UserDtoWWg n
userWWo s
)WWs t
{XX 
varYY 
appUserYY 
=YY 
newYY 
AppUserYY !
{ZZ 	
UserName[[ 
=[[ 
user[[ 
.[[ 
User[[  
.[[  !
Name[[! %
,[[% &
NormalizedUserName\\ 
=\\  
user\\! %
.\\% &
User\\& *
.\\* +
Name\\+ /
.\\/ 0
ToUpper\\0 7
(\\7 8
CultureInfo\\8 C
.\\C D
InvariantCulture\\D T
)\\T U
,\\U V
Email]] 
=]] 
	EmailStub]] 
,]] 
ConcurrencyStamp^^ 
=^^ 
Guid^^ #
.^^# $
NewGuid^^$ +
(^^+ ,
)^^, -
.^^- .
ToString^^. 6
(^^6 7
)^^7 8
,^^8 9
}__ 	
;__	 

awaitaa 
userManageraa 
.aa 
CreateAsyncaa %
(aa% &
appUseraa& -
)aa- .
;aa. /
awaitbb '
AddUserToSelectedRolesAsyncbb )
(bb) *
userManagerbb* 5
,bb5 6
roleManagerbb7 B
,bbB C
userbbD H
,bbH I
appUserbbJ Q
)bbQ R
;bbR S
awaitcc  
AddUserPasswordAsynccc "
(cc" #
userManagercc# .
,cc. /
usercc0 4
,cc4 5
appUsercc6 =
)cc= >
;cc> ?
}dd 
privateff 
staticff 
asyncff 
Taskff  
UpdateUserRolesAsyncff 2
(ff2 3
UserManagerff3 >
<ff> ?
AppUserff? F
>ffF G
userManagerffH S
,ffS T
RoleManagerffU `
<ff` a
AppRoleffa h
>ffh i
roleManagerffj u
,ffu v
UserDtoffw ~
user	ff �
,
ff� �
AppUser
ff� �
userToUpdate
ff� �
)
ff� �
{gg 
awaithh '
RemoveUserFromAllRolesAsynchh )
(hh) *
userManagerhh* 5
,hh5 6
userToUpdatehh7 C
)hhC D
;hhD E
awaitii '
AddUserToSelectedRolesAsyncii )
(ii) *
userManagerii* 5
,ii5 6
roleManagerii7 B
,iiB C
useriiD H
,iiH I
userToUpdateiiJ V
)iiV W
;iiW X
}jj 
privatell 
staticll 
asyncll 
Taskll '
AddUserToSelectedRolesAsyncll 9
(ll9 :
UserManagerll: E
<llE F
AppUserllF M
>llM N
userManagerllO Z
,llZ [
RoleManagerll\ g
<llg h
AppRolellh o
>llo p
roleManagerllq |
,ll| }
UserDto	ll~ �
user
ll� �
,
ll� �
AppUser
ll� �
userToUpdate
ll� �
)
ll� �
{mm 
foreachnn 
(nn 
varnn 
roleIdnn 
innn 
usernn #
.nn# $
Rolesnn$ )
)nn) *
{oo 	
varpp 
rolepp 
=pp 
awaitpp 
roleManagerpp (
.pp( )
Rolespp) .
.pp. /
FirstOrDefaultAsyncpp/ B
(ppB C
xppC D
=>ppE G
xppH I
.ppI J
IdppJ L
==ppM O
roleIdppP V
)ppV W
;ppW X
ifrr 
(rr 
rolerr 
isrr 
notrr 
nullrr  
)rr  !
{ss 
awaittt 
userManagertt !
.tt! "
AddToRoleAsynctt" 0
(tt0 1
userToUpdatett1 =
,tt= >
rolett? C
.ttC D
NamettD H
!ttH I
)ttI J
;ttJ K
}uu 
}vv 	
}ww 
privateyy 
staticyy 
asyncyy 
Taskyy '
RemoveUserFromAllRolesAsyncyy 9
(yy9 :
UserManageryy: E
<yyE F
AppUseryyF M
>yyM N
userManageryyO Z
,yyZ [
AppUseryy\ c
userToUpdateyyd p
)yyp q
{zz 
var{{ 
roles{{ 
={{ 
await{{ 
userManager{{ %
.{{% &
GetRolesAsync{{& 3
({{3 4
userToUpdate{{4 @
){{@ A
;{{A B
if|| 

(|| 
roles|| 
!=|| 
null|| 
|||| 
roles|| "
!||" #
.||# $
Any||$ '
(||' (
)||( )
)||) *
{}} 	
var~~ 
result~~ 
=~~ 
await~~ 
userManager~~ *
.~~* + 
RemoveFromRolesAsync~~+ ?
(~~? @
userToUpdate~~@ L
,~~L M
roles~~N S
!~~S T
)~~T U
;~~U V
if 
( 
! 
result 
. 
	Succeeded !
)! "
{
�� 
throw
�� 
new
��  
GamestoreException
�� ,
(
��, -
$str
��- J
)
��J K
;
��K L
}
�� 
}
�� 	
}
�� 
private
�� 
static
�� 
void
�� 
UpdateUserDetails
�� )
(
��) *
UserDto
��* 1
user
��2 6
,
��6 7
AppUser
��8 ?
?
��? @
userToUpdate
��A M
)
��M N
{
�� 
userToUpdate
�� 
.
�� 
UserName
�� 
=
�� 
user
��  $
.
��$ %
User
��% )
.
��) *
Name
��* .
;
��. /
userToUpdate
�� 
.
��  
NormalizedUserName
�� '
=
��( )
user
��* .
.
��. /
User
��/ 3
.
��3 4
Name
��4 8
.
��8 9
ToUpper
��9 @
(
��@ A
CultureInfo
��A L
.
��L M
InvariantCulture
��M ]
)
��] ^
;
��^ _
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� %
UpdateUserPasswordAsync
�� 5
(
��5 6
UserManager
��6 A
<
��A B
AppUser
��B I
>
��I J
userManager
��K V
,
��V W
UserDto
��X _
user
��` d
,
��d e
AppUser
��f m
userToUpdate
��n z
)
��z {
{
�� 
await
�� %
RemoveUserPasswordAsync
�� %
(
��% &
userManager
��& 1
,
��1 2
userToUpdate
��3 ?
)
��? @
;
��@ A
await
�� "
AddUserPasswordAsync
�� "
(
��" #
userManager
��# .
,
��. /
user
��0 4
,
��4 5
userToUpdate
��6 B
)
��B C
;
��C D
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� "
AddUserPasswordAsync
�� 2
(
��2 3
UserManager
��3 >
<
��> ?
AppUser
��? F
>
��F G
userManager
��H S
,
��S T
UserDto
��U \
user
��] a
,
��a b
AppUser
��c j
userToUpdate
��k w
)
��w x
{
�� 
var
�� 
addPasswordResult
�� 
=
�� 
await
��  %
userManager
��& 1
.
��1 2
AddPasswordAsync
��2 B
(
��B C
userToUpdate
��C O
,
��O P
user
��Q U
.
��U V
Password
��V ^
)
��^ _
;
��_ `
if
�� 

(
�� 
!
�� 
addPasswordResult
�� 
.
�� 
	Succeeded
�� (
)
��( )
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$str
��) A
)
��A B
;
��B C
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� %
RemoveUserPasswordAsync
�� 5
(
��5 6
UserManager
��6 A
<
��A B
AppUser
��B I
>
��I J
userManager
��K V
,
��V W
AppUser
��X _
userToUpdate
��` l
)
��l m
{
�� 
var
�� "
removePasswordResult
��  
=
��! "
await
��# (
userManager
��) 4
.
��4 5!
RemovePasswordAsync
��5 H
(
��H I
userToUpdate
��I U
)
��U V
;
��V W
if
�� 

(
�� 
!
�� "
removePasswordResult
�� !
.
��! "
	Succeeded
��" +
)
��+ ,
{
�� 	
throw
�� 
new
��  
GamestoreException
�� (
(
��( )
$str
��) D
)
��D E
;
��E F
}
�� 	
}
�� 
private
�� 
static
�� 
async
�� 
Task
�� "
FindRolesByUserAsync
�� 2
(
��2 3
UserManager
��3 >
<
��> ?
AppUser
��? F
>
��F G
userManager
��H S
,
��S T
RoleManager
��U `
<
��` a
AppRole
��a h
>
��h i
roleManager
��j u
,
��u v
AppUser
��w ~
user�� �
,��� �
List��� �
<��� �
UserRoleDto��� �
>��� �
	userRoles��� �
)��� �
{
�� 
var
�� 
roles
�� 
=
�� 
await
�� 
userManager
�� %
.
��% &
GetRolesAsync
��& 3
(
��3 4
user
��4 8
)
��8 9
;
��9 :
foreach
�� 
(
�� 
var
�� 
role
�� 
in
�� 
roles
�� "
)
��" #
{
�� 	
var
�� 
name
�� 
=
�� 
role
�� 
;
�� 
var
�� 
appRole
�� 
=
�� 
await
�� 
roleManager
��  +
.
��+ ,
FindByNameAsync
��, ;
(
��; <
role
��< @
)
��@ A
;
��A B
var
�� 
id
�� 
=
�� 
await
�� 
roleManager
�� &
.
��& '
GetRoleIdAsync
��' 5
(
��5 6
appRole
��6 =
!
��= >
)
��> ?
;
��? @
	userRoles
�� 
.
�� 
Add
�� 
(
�� 
new
�� 
UserRoleDto
�� )
{
��* +
Id
��, .
=
��/ 0
id
��1 3
,
��3 4
Name
��5 9
=
��: ;
name
��< @
}
��A B
)
��B C
;
��C D
}
�� 	
}
�� 
}�� �
aD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Stubs\CustomerStub.cs
	namespace 	
	Gamestore
 
. 
WebApi 
. 
Stubs  
;  !
public 
class 
CustomerStub 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
=! "
new# &
Guid' +
(+ ,
$str, R
)R S
;S T
public 

static 
string 
Name 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
$str. 5
;5 6
public		 

static		 
DateTime		 

BannedTill		 %
{		& '
get		( +
;		+ ,
set		- 0
;		0 1
}		2 3
}

 �
rD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\CommentModelDtoValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal 
class	 $
CommentModelDtoValidator '
:( )
AbstractValidator* ;
<; <
CommentModelDto< K
>K L
{ 
internal $
CommentModelDtoValidator %
(% &
)& '
{		 
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Comment

 
.

 
Body

 #
)

# $
.

$ %
NotNull

% ,
(

, -
)

- .
.

. /
WithMessage

/ :
(

: ;
$str

; I
)

I J
;

J K
} 
} � 
qD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\GameDtoWrapperValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal 
class	 #
GameDtoWrapperValidator &
:' (
AbstractValidator) :
<: ;
GameDtoWrapper; I
>I J
{ 
internal #
GameDtoWrapperValidator $
($ %
)% &
{		 
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Game

 
)

 
.

 
NotNull

 $
(

$ %
)

% &
.

& '
WithMessage

' 2
(

2 3
$str

3 H
)

H I
;

I J
RuleFor 
( 
x 
=> 
x 
. 
	Publisher  
)  !
.! "
NotEmpty" *
(* +
)+ ,
., -
WithMessage- 8
(8 9
$str9 W
)W X
;X Y
RuleFor 
( 
x 
=> 
x 
. 
	Platforms  
)  !
.! "
NotEmpty" *
(* +
)+ ,
., -
WithMessage- 8
(8 9
$str9 \
)\ ]
;] ^
RuleFor 
( 
x 
=> 
x 
. 
Genres 
) 
. 
NotEmpty '
(' (
)( )
.) *
WithMessage* 5
(5 6
$str6 V
)V W
;W X
RuleFor 
( 
x 
=> 
x 
. 
Game 
. 
Name  
)  !
.! "
NotNull" )
() *
)* +
.+ ,
WithMessage, 7
(7 8
$str8 F
)F G
;G H
RuleFor 
( 
x 
=> 
x 
. 
Game 
. 
Discontinued (
)( )
.) * 
GreaterThanOrEqualTo* >
(> ?
$num? @
)@ A
.A B
WithMessageB M
(M N
$strN e
)e f
;f g
RuleFor 
( 
x 
=> 
x 
. 
Game 
. 
Discontinued (
)( )
.) *
LessThanOrEqualTo* ;
(; <
$num< ?
)? @
.@ A
WithMessageA L
(L M
$strM h
)h i
;i j
RuleFor 
( 
x 
=> 
x 
. 
Game 
. 
Price !
)! "
." #
GreaterThan# .
(. /
$num/ 0
)0 1
.1 2
WithMessage2 =
(= >
$str> S
)S T
;T U
RuleFor 
( 
x 
=> 
x 
. 
Game 
. 
UnitInStock '
)' (
.( ) 
GreaterThanOrEqualTo) =
(= >
$num> ?
)? @
.@ A
WithMessageA L
(L M
$strM b
)b c
;c d
RuleFor 
( 
x 
=> 
x 
. 
Game 
. 
Name  
)  !
.! "
Must" &
(& '
companyName' 2
=>3 5
{ 	
return 
! 
string 
. 
IsNullOrEmpty (
(( )
companyName) 4
)4 5
;5 6
} 	
)	 

.
 
WithMessage 
( 
$str 6
)6 7
;7 8
} 
} �+
uD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\GenreDtoWrapperAddValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal		 
class			 '
GenreDtoWrapperAddValidator		 *
:		+ ,
AbstractValidator		- >
<		> ?
GenreModelDto		? L
>		L M
{

 
internal '
GenreDtoWrapperAddValidator (
(( )
IUnitOfWork) 4

unitOfWork5 ?
)? @
{ 
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
NotEmpty %
(% &
)& '
.' (
WithMessage( 3
(3 4
$str4 B
)B C
;C D
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
Must !
(! "
companyName" -
=>. 0
{ 	
return 
! 
string 
. 
IsNullOrEmpty (
(( )
companyName) 4
)4 5
;5 6
} 	
)	 

.
 
WithMessage 
( 
$str 6
)6 7
;7 8
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
	MustAsync &
(& '
async' ,
(- .
name. 2
,2 3
cancellation4 @
)@ A
=>B D
{ 	
var 
genres 
= 
await 

unitOfWork )
.) *
GenreRepository* 9
.9 :
GetAllAsync: E
(E F
)F G
;G H
var 
exisitngGenres 
=  
genres! '
.' (
Where( -
(- .
x. /
=>0 2
x3 4
.4 5
Name5 9
==: <
name= A
)A B
;B C
return 
! 
exisitngGenres "
." #
Any# &
(& '
)' (
;( )
} 	
)	 

.
 
WithMessage 
( 
$str 2
)2 3
;3 4
RuleFor 
( 
x 
=> 
x 
. 
ParentGenreId $
)$ %
.% &
	MustAsync& /
(/ 0
async0 5
(6 7
id7 9
,9 :
cancellation; G
)G H
=>I K
{ 	
if 
( 
id 
!= 
null 
) 
{ 
var 
genres 
= 
await "

unitOfWork# -
.- .
GenreRepository. =
.= >
GetAllAsync> I
(I J
)J K
;K L
var 
exisitngGenres "
=# $
genres% +
.+ ,
Where, 1
(1 2
x2 3
=>4 6
x7 8
.8 9
Id9 ;
==< >
id? A
)A B
;B C
return 
exisitngGenres %
.% &
Any& )
() *
)* +
;+ ,
}   
else!! 
{"" 
return## 
true## 
;## 
}$$ 
}%% 	
)%%	 

.%%
 
WithMessage%% 
(%% 
$str%% 8
)%%8 9
;%%9 :
RuleFor&& 
(&& 
x&& 
=>&& 
new&& 
{&& 
x&& 
.&& 
Id&& 
,&&  
x&&! "
.&&" #
ParentGenreId&&# 0
}&&1 2
)&&2 3
.&&3 4
	MustAsync&&4 =
(&&= >
async&&> C
(&&D E
data&&E I
,&&I J
cancellation&&K W
)&&W X
=>&&Y [
{'' 	
var(( 
genres(( 
=(( 
await(( 

unitOfWork(( )
.(() *
GenreRepository((* 9
.((9 :
GetAllAsync((: E
(((E F
)((F G
;((G H
List)) 
<)) 
Genre)) 
>)) 
forbiddenList)) %
=))& '
[))( )
]))) *
;))* +
if++ 
(++ 
data++ 
.++ 
Id++ 
!=++ 
null++ 
)++  
{,, 
ValidationHelpers-- !
.--! "!
CyclicReferenceHelper--" 7
(--7 8
genres--8 >
,--> ?
forbiddenList--@ M
,--M N
(--O P
Guid--P T
)--T U
data--U Y
.--Y Z
Id--Z \
)--\ ]
;--] ^
}.. 
return00 
!00 
forbiddenList00 !
.00! "
Exists00" (
(00( )
x00) *
=>00+ -
x00. /
.00/ 0
Id000 2
==003 5
data006 :
.00: ;
ParentGenreId00; H
)00H I
;00I J
}11 	
)11	 

.11
 
WithMessage11 
(11 
$str11 6
)116 7
;117 8
}22 
}33 �>
xD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\GenreDtoWrapperUpdateValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal		 
class			 *
GenreDtoWrapperUpdateValidator		 -
:		. /
AbstractValidator		0 A
<		A B
GenreModelDto		B O
>		O P
{

 
internal *
GenreDtoWrapperUpdateValidator +
(+ ,
IUnitOfWork, 7

unitOfWork8 B
)B C
{ 
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
NotEmpty %
(% &
)& '
.' (
WithMessage( 3
(3 4
$str4 B
)B C
;C D
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
Must !
(! "
companyName" -
=>. 0
{ 	
return 
! 
string 
. 
IsNullOrEmpty (
(( )
companyName) 4
)4 5
;5 6
} 	
)	 

.
 
WithMessage 
( 
$str 6
)6 7
;7 8
RuleFor 
( 
x 
=> 
new 
{ 
x 
. 
Name !
,! "
x# $
.$ %
Id% '
}( )
)) *
.* +
	MustAsync+ 4
(4 5
async5 :
(; <
data< @
,@ A
cancellationB N
)N O
=>P R
{ 	
var 
genres 
= 
await 

unitOfWork )
.) *
GenreRepository* 9
.9 :
GetAllAsync: E
(E F
)F G
;G H
var 
exisitngGenres 
=  
genres! '
.' (
Where( -
(- .
x. /
=>0 2
x3 4
.4 5
Name5 9
==: <
data= A
.A B
NameB F
&&G I
xJ K
.K L
IdL N
!=O Q
dataR V
.V W
IdW Y
)Y Z
;Z [
return 
! 
exisitngGenres "
." #
Any# &
(& '
)' (
;( )
} 	
)	 

.
 
WithMessage 
( 
$str 1
)1 2
;2 3
RuleFor 
( 
x 
=> 
x 
. 
ParentGenreId $
)$ %
.% &
	MustAsync& /
(/ 0
async0 5
(6 7
id7 9
,9 :
cancellation; G
)G H
=>I K
{ 	
if 
( 
id 
!= 
null 
) 
{ 
var 
genres 
= 
await "

unitOfWork# -
.- .
GenreRepository. =
.= >
GetAllAsync> I
(I J
)J K
;K L
var 
exisitngGenres "
=# $
genres% +
.+ ,
Where, 1
(1 2
x2 3
=>4 6
x7 8
.8 9
Id9 ;
==< >
id? A
)A B
;B C
return 
exisitngGenres %
.% &
Any& )
() *
)* +
;+ ,
}   
else!! 
{"" 
return## 
true## 
;## 
}$$ 
}%% 	
)%%	 

.%%
 
WithMessage%% 
(%% 
$str%% 8
)%%8 9
;%%9 :
RuleFor&& 
(&& 
x&& 
=>&& 
new&& 
{&& 
x&& 
.&& 
Id&& 
,&&  
x&&! "
.&&" #
ParentGenreId&&# 0
}&&1 2
)&&2 3
.&&3 4
	MustAsync&&4 =
(&&= >
async&&> C
(&&D E
data&&E I
,&&I J
cancellation&&K W
)&&W X
=>&&Y [
{'' 	
var(( 
genres(( 
=(( 
await(( 

unitOfWork(( )
.(() *
GenreRepository((* 9
.((9 :
GetAllAsync((: E
(((E F
)((F G
;((G H
var)) 
existingGenres)) 
=))  
genres))! '
.))' (
Where))( -
())- .
x)). /
=>))0 2
x))3 4
.))4 5
ParentGenreId))5 B
==))C E
data))F J
.))J K
Id))K M
&&))N P
x))Q R
.))R S
Id))S U
==))V X
data))Y ]
.))] ^
ParentGenreId))^ k
)))k l
;))l m
return** 
!** 
existingGenres** "
.**" #
Any**# &
(**& '
)**' (
;**( )
}++ 	
)++	 

.++
 
WithMessage++ 
(++ 
$str++ ]
)++] ^
;++^ _
RuleFor,, 
(,, 
x,, 
=>,, 
new,, 
{,, 
x,, 
.,, 
Id,, 
,,,  
x,,! "
.,," #
ParentGenreId,,# 0
},,1 2
),,2 3
.,,3 4
	MustAsync,,4 =
(,,= >
async,,> C
(,,D E
data,,E I
,,,I J
cancellation,,K W
),,W X
=>,,Y [
{-- 	
var.. 
genres.. 
=.. 
await.. 

unitOfWork.. )
...) *
GenreRepository..* 9
...9 :
GetAllAsync..: E
(..E F
)..F G
;..G H
List// 
<// 
Genre// 
>// 
forbiddenList// %
=//& '
[//( )
]//) *
;//* +
if11 
(11 
data11 
.11 
Id11 
!=11 
null11 
)11  
{22 
ValidationHelpers33 !
.33! "!
CyclicReferenceHelper33" 7
(337 8
genres338 >
,33> ?
forbiddenList33@ M
,33M N
(33O P
Guid33P T
)33T U
data33U Y
.33Y Z
Id33Z \
)33\ ]
;33] ^
}44 
return66 
!66 
forbiddenList66 !
.66! "
Exists66" (
(66( )
x66) *
=>66+ -
x66. /
.66/ 0
Id660 2
==663 5
data666 :
.66: ;
ParentGenreId66; H
)66H I
;66I J
}77 	
)77	 

.77
 
WithMessage77 
(77 
$str77 6
)776 7
;777 8
RuleFor88 
(88 
x88 
=>88 
new88 
{88 
x88 
.88 
Id88 
,88  
x88! "
.88" #
ParentGenreId88# 0
}881 2
)882 3
.883 4
Must884 8
(888 9
(889 :
data88: >
,88> ?
cancellation88@ L
)88L M
=>88N P
{99 	
return:: 
data:: 
.:: 
Id:: 
!=:: 
data:: "
.::" #
ParentGenreId::# 0
;::0 1
};; 	
);;	 

.;;
 
WithMessage;; 
(;; 
$str;; F
);;F G
;;;G H
}<< 
}== �
uD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\PlatformDtoWrapperValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal 
class	 '
PlatformDtoWrapperValidator *
:+ ,
AbstractValidator- >
<> ?
PlatformModelDto? O
>O P
{ 
internal		 '
PlatformDtoWrapperValidator		 (
(		( )
IUnitOfWork		) 4

unitOfWork		5 ?
)		? @
{

 
RuleFor 
( 
x 
=> 
x 
. 
Type 
) 
. 
NotEmpty %
(% &
)& '
.' (
WithMessage( 3
(3 4
$str4 B
)B C
;C D
RuleFor 
( 
x 
=> 
x 
. 
Type 
) 
. 
Must !
(! "
type" &
=>' )
{ 	
return 
! 
string 
. 
IsNullOrEmpty (
(( )
type) -
)- .
;. /
} 	
)	 

.
 
WithMessage 
( 
$str 6
)6 7
;7 8
RuleFor 
( 
x 
=> 
x 
. 
Type 
) 
. 
	MustAsync &
(& '
async' ,
(- .
type. 2
,2 3
cancellation4 @
)@ A
=>B D
{ 	
var 
	platforms 
= 
await !

unitOfWork" ,
., -
PlatformRepository- ?
.? @
GetAllAsync@ K
(K L
)L M
;M N
var 
exisitngPlatforms !
=" #
	platforms$ -
.- .
Where. 3
(3 4
x4 5
=>6 8
x9 :
.: ;
Type; ?
==@ B
typeC G
)G H
;H I
return 
! 
exisitngPlatforms %
.% &
Any& )
() *
)* +
;+ ,
} 	
)	 

.
 
WithMessage 
( 
$str 5
)5 6
;6 7
} 
} �
vD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\PublisherDtoWrapperValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal 
class	 (
PublisherDtoWrapperValidator +
:, -
AbstractValidator. ?
<? @
PublisherModelDto@ Q
>Q R
{ 
public		 
(
PublisherDtoWrapperValidator		 '
(		' (
IUnitOfWork		( 3

unitOfWork		4 >
)		> ?
{

 
RuleFor 
( 
x 
=> 
x 
. 
CompanyName "
)" #
.# $
NotNull$ +
(+ ,
), -
.- .
WithMessage. 9
(9 :
$str: L
)L M
;M N
RuleFor 
( 
x 
=> 
x 
. 
CompanyName "
)" #
.# $
Must$ (
(( )
companyName) 4
=>5 7
{ 	
return 
! 
string 
. 
IsNullOrEmpty (
(( )
companyName) 4
)4 5
;5 6
} 	
)	 

.
 
WithMessage 
( 
$str >
)> ?
;? @
RuleFor 
( 
x 
=> 
x 
. 
CompanyName "
)" #
.# $
	MustAsync$ -
(- .
async. 3
(4 5
companyName5 @
,@ A
cancellationB N
)N O
=>P R
{ 	
var 
existingPublisher !
=" #
await$ )

unitOfWork* 4
.4 5
PublisherRepository5 H
.H I!
GetByCompanyNameAsyncI ^
(^ _
companyName_ j
)j k
;k l
return 
existingPublisher $
==% '
null( ,
;, -
} 	
)	 

.
 
WithMessage 
( 
$str A
)A B
;B C
} 
} �
nD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.Services\Validation\VisaPaymentValidator.cs
	namespace 	
	Gamestore
 
. 
BLL 
. 

Validation "
;" #
internal 
class	  
VisaPaymentValidator #
:$ %
AbstractValidator& 7
<7 8
PaymentModelDto8 G
>G H
{ 
internal  
VisaPaymentValidator !
(! "
)" #
{		 
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Model

 
.

 

CardNumber

 '
)

' (
.

( )
NotEmpty

) 1
(

1 2
)

2 3
.

3 4
WithMessage

4 ?
(

? @
$str

@ U
)

U V
;

V W
RuleFor 
( 
x 
=> 
x 
. 
Model 
. 
MonthExpire (
)( )
.) *
NotEmpty* 2
(2 3
)3 4
.4 5
WithMessage5 @
(@ A
$strA [
)[ \
;\ ]
RuleFor 
( 
x 
=> 
x 
. 
Model 
. 

YearExpire '
)' (
.( )
NotEmpty) 1
(1 2
)2 3
.3 4
WithMessage4 ?
(? @
$str@ Y
)Y Z
;Z [
RuleFor 
( 
x 
=> 
x 
. 
Model 
. 
Cvv2 !
)! "
." #
NotEmpty# +
(+ ,
), -
.- .
WithMessage. 9
(9 :
$str: H
)H I
;I J
RuleFor 
( 
x 
=> 
x 
. 
Model 
. 
Holder #
)# $
.$ %
NotEmpty% -
(- .
). /
./ 0
WithMessage0 ;
(; <
$str< V
)V W
;W X
} 
} 