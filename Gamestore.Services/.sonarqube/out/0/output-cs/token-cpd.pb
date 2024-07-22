ﬂ 
}D:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\DIRegistrations\MongoRepositoryServices.cs
	namespace		 	
	Gamestore		
 
.		 
MongoRepository		 #
.		# $
DIRegistrations		$ 3
;		3 4
public 
static 
class #
MongoRepositoryServices +
{ 
public 

static 
void 
	Configure  
(  !
IServiceCollection! 3
services4 <
,< =!
IConfigurationSection> S

connectionT ^
)^ _
{ 
services 
. 
	Configure 
<  
MongoDBSettingsModel /
>/ 0
(0 1

connection1 ;
); <
;< =
services 
. 
AddSingleton 
< 
IMongoClient *
,* +
MongoClient, 7
>7 8
(8 9
sp9 ;
=>< >
{ 	
var 
settings 
= 
sp 
. 
GetRequiredService 0
<0 1
IOptions1 9
<9 : 
MongoDBSettingsModel: N
>N O
>O P
(P Q
)Q R
.R S
ValueS X
;X Y
return 
new 
MongoClient "
(" #
settings# +
.+ ,
ConnectionString, <
)< =
;= >
} 	
)	 

;
 
services 
. 
	AddScoped 
( 
sp 
=>  
{ 	
var 
settings 
= 
sp 
. 
GetRequiredService 0
<0 1
IOptions1 9
<9 : 
MongoDBSettingsModel: N
>N O
>O P
(P Q
)Q R
.R S
ValueS X
;X Y
var 
client 
= 
sp 
. 
GetRequiredService .
<. /
IMongoClient/ ;
>; <
(< =
)= >
;> ?
return 
client 
. 
GetDatabase %
(% &
settings& .
.. /
DatabaseName/ ;
); <
;< =
} 	
)	 

;
 
services 
. 
	AddScoped 
< 
IProductRepository -
,- .
ProductRepository/ @
>@ A
(A B
)B C
;C D
services 
. 
	AddScoped 
< 
ICategoryRepository .
,. /
CategoryRepository0 B
>B C
(C D
)D E
;E F
services 
. 
	AddScoped 
< 
ISupplierRepository .
,. /
SupplierRepository0 B
>B C
(C D
)D E
;E F
services 
. 
	AddScoped 
< 
IShipperRepository -
,- .
ShipperRepository/ @
>@ A
(A B
)B C
;C D
services 
. 
	AddScoped 
< 
IOrderRepository +
,+ ,
OrderRepository- <
>< =
(= >
)> ?
;? @
services   
.   
	AddScoped   
<   "
IOrderDetailRepository   1
,  1 2!
OrderDetailRepository  3 H
>  H I
(  I J
)  J K
;  K L
services!! 
.!! 
	AddScoped!! 
<!! 
IMongoUnitOfWork!! +
,!!+ ,
MongoUnitOfWork!!- <
>!!< =
(!!= >
)!!> ?
;!!? @
services"" 
."" 
	AddScoped"" 
<"" 
ILogRepository"" )
,"") *
LogRepository""+ 8
>""8 9
(""9 :
)"": ;
;""; <
}## 
}$$ ∫
nD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\GameAddLogEntry.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class 
GameAddLogEntry 
{ 
[		 
BsonId		 
]		 
[

 
BsonRepresentation

 
(

 
BsonType

  
.

  !
ObjectId

! )
)

) *
]

* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
Action 
{ 
get 
; 
set  #
;# $
}% &
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 

EntityType 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
Value 
{ 
get 
; 
set "
;" #
}$ %
[ 
BsonElement 
( 
$str 
) 
]  
public 

string 
PublisherId 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
) 
] 
public 

List 
< 

MongoGenre 
> 
Genres "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 
BsonElement 
( 
$str 
) 
] 
public 

List 
< 
MongoPlatform 
> 
	Platforms (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
[ 
BsonElement 
( 
$str 
) 
] 
public   

DateTime   
Date   
{   
get   
;   
set    #
;  # $
}  % &
}!!  
qD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\GameDeleteLogEntry.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class 
GameDeleteLogEntry 
{ 
[ 
BsonId 
] 
[		 
BsonRepresentation		 
(		 
BsonType		  
.		  !
ObjectId		! )
)		) *
]		* +
public

 

string

 
ObjectId

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
Action 
{ 
get 
; 
set  #
;# $
}% &
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 

EntityType 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
Value 
{ 
get 
; 
set "
;" #
}$ %
[ 
BsonElement 
( 
$str 
) 
] 
public 

DateTime 
Date 
{ 
get 
; 
set  #
;# $
}% &
} Œ
qD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\GameUpdateLogEntry.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class 
GameUpdateLogEntry 
{ 
[		 
BsonId		 
]		 
[

 
BsonRepresentation

 
(

 
BsonType

  
.

  !
ObjectId

! )
)

) *
]

* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
Action 
{ 
get 
; 
set  #
;# $
}% &
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 

EntityType 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
OldValue 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
NewValue 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str !
)! "
]" #
public 

string 
OldPublisherId  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 
BsonElement 
( 
$str !
)! "
]" #
public 

string 
NewPublisherId  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 
BsonElement 
( 
$str 
) 
] 
public   

List   
<   

MongoGenre   
>   
	OldGenres   %
{  & '
get  ( +
;  + ,
set  - 0
;  0 1
}  2 3
["" 
BsonElement"" 
("" 
$str"" 
)""  
]""  !
public## 

List## 
<## 
MongoPlatform## 
>## 
OldPlatforms## +
{##, -
get##. 1
;##1 2
set##3 6
;##6 7
}##8 9
[%% 
BsonElement%% 
(%% 
$str%% 
)%% 
]%% 
public&& 

List&& 
<&& 

MongoGenre&& 
>&& 
	NewGenres&& %
{&&& '
get&&( +
;&&+ ,
set&&- 0
;&&0 1
}&&2 3
[(( 
BsonElement(( 
((( 
$str(( 
)((  
]((  !
public)) 

List)) 
<)) 
MongoPlatform)) 
>)) 
NewPlatforms)) +
{)), -
get)). 1
;))1 2
set))3 6
;))6 7
}))8 9
[++ 
BsonElement++ 
(++ 
$str++ 
)++ 
]++ 
public,, 

DateTime,, 
Date,, 
{,, 
get,, 
;,, 
set,,  #
;,,# $
},,% &
}-- É

lD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoCategory.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
[ #
BsonIgnoreExtraElements 
] 
public 
class 
MongoCategory 
{ 
[		 
BsonId		 
]		 
[

 
BsonRepresentation

 
(

 
BsonType

  
.

  !
ObjectId

! )
)

) *
]

* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 

CategoryId 
{ 
get 
;  
set! $
;$ %
}& '
[ 
BsonElement 
( 
$str 
)  
]  !
public 

string 
CategoryName 
{  
get! $
;$ %
set& )
;) *
}+ ,
} ø"
iD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoOrder.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
[ #
BsonIgnoreExtraElements 
] 
public 
class 

MongoOrder 
{ 
[		 
BsonId		 
]		 
[

 
BsonRepresentation

 
(

 
BsonType

  
.

  !
ObjectId

! )
)

) *
]

* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
OrderId 
{ 
get 
; 
set !
;! "
}# $
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 

CustomerId 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 

EmployeeId 
{ 
get 
;  
set! $
;$ %
}& '
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
	OrderDate 
{ 
get !
;! "
set# &
;& '
}( )
[ 
BsonElement 
( 
$str 
)  
]  !
public 

string 
RequireDate 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
) 
]  
public 

string 
ShippedDate 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
) 
] 
public   

int   
ShipVia   
{   
get   
;   
set   !
;  ! "
}  # $
["" 
BsonElement"" 
("" 
$str"" 
)"" 
]"" 
public## 

double## 
Freight## 
{## 
get## 
;##  
set##! $
;##$ %
}##& '
[%% 
BsonElement%% 
(%% 
$str%% 
)%% 
]%% 
public&& 

string&& 
ShipName&& 
{&& 
get&&  
;&&  !
set&&" %
;&&% &
}&&' (
[(( 
BsonElement(( 
((( 
$str(( 
)(( 
]((  
public)) 

dynamic)) 
ShipAddress)) 
{))  
get))! $
;))$ %
set))& )
;))) *
}))+ ,
[++ 
BsonElement++ 
(++ 
$str++ 
)++ 
]++ 
public,, 

dynamic,, 
ShipCity,, 
{,, 
get,, !
;,,! "
set,,# &
;,,& '
},,( )
[.. 
BsonElement.. 
(.. 
$str.. 
).. 
].. 
public// 

dynamic// 
?// 

ShipRegion// 
{//  
get//! $
;//$ %
set//& )
;//) *
}//+ ,
[11 
BsonElement11 
(11 
$str11 !
)11! "
]11" #
public22 

dynamic22 
ShipPostalCode22 !
{22" #
get22$ '
;22' (
set22) ,
;22, -
}22. /
[44 
BsonElement44 
(44 
$str44 
)44 
]44  
public55 

dynamic55 
ShipCountry55 
{55  
get55! $
;55$ %
set55& )
;55) *
}55+ ,
}66 Ñ
oD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoOrderDetail.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
[ #
BsonIgnoreExtraElements 
] 
public 
class 
MongoOrderDetail 
{ 
[		 
BsonId		 
]		 
[

 
BsonRepresentation

 
(

 
BsonType

  
.

  !
ObjectId

! )
)

) *
]

* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
OrderId 
{ 
get 
; 
set !
;! "
}# $
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
	ProductId 
{ 
get 
; 
set  #
;# $
}% &
[ 
BsonElement 
( 
$str 
) 
] 
public 

double 
	UnitPrice 
{ 
get !
;! "
set# &
;& '
}( )
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
Quantity 
{ 
get 
; 
set "
;" #
}$ %
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
Discount 
{ 
get 
; 
set "
;" #
}$ %
} ≤.
kD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoProduct.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
[ #
BsonIgnoreExtraElements 
] 
public 
class 
MongoProduct 
{		 
[

 
BsonId

 
]

 
[ 
BsonRepresentation 
( 
BsonType  
.  !
ObjectId! )
)) *
]* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
	ProductId 
{ 
get 
; 
set  #
;# $
}% &
[ 

BsonIgnore 
] 
public 

Guid 
ProductIdGuid 
{ 
get 
{ 	
return 
GuidHelpers 
. 
	IntToGuid (
(( )
	ProductId) 2
)2 3
;3 4
} 	
} 
[ 
BsonElement 
( 
$str 
) 
]  
public 

string 
ProductName 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 

SupplierID 
{ 
get 
;  
set! $
;$ %
}& '
[   

BsonIgnore   
]   
public!! 

MongoPublisher!! 
Supplier!! "
{"" 
get## 
{$$ 	
return%% 
new%% 
MongoPublisher%% %
(%%% &
)%%& '
{%%( )
Id%%* ,
=%%- .
GuidHelpers%%/ :
.%%: ;
	IntToGuid%%; D
(%%D E

SupplierID%%E O
)%%O P
}%%Q R
;%%R S
}&& 	
}'' 
[)) 
BsonElement)) 
()) 
$str)) 
))) 
])) 
public** 

int** 

CategoryID** 
{** 
get** 
;**  
set**! $
;**$ %
}**& '
[,, 

BsonIgnore,, 
],, 
public-- 

Guid-- 
CategoryIDGuid-- 
{.. 
get// 
{00 	
return11 
GuidHelpers11 
.11 
	IntToGuid11 (
(11( )

CategoryID11) 3
)113 4
;114 5
}22 	
}33 
[55 
BsonElement55 
(55 
$str55 
)55 
]55 
public66 

double66 
	UnitPrice66 
{66 
get66 !
;66! "
set66# &
;66& '
}66( )
[88 
BsonElement88 
(88 
$str88 
)88  
]88  !
public99 

int99 
UnitsInStock99 
{99 
get99 !
;99! "
set99# &
;99& '
}99( )
[;; 
BsonElement;; 
(;; 
$str;; 
);;  
];;  !
public<< 

int<< 
UnitsOnOrder<< 
{<< 
get<< !
;<<! "
set<<# &
;<<& '
}<<( )
[>> 
BsonElement>> 
(>> 
$str>> 
)>>  
]>>  !
public?? 

int?? 
ReorderLevel?? 
{?? 
get?? !
;??! "
set??# &
;??& '
}??( )
[AA 
BsonElementAA 
(AA 
$strAA 
)AA  
]AA  !
publicBB 

intBB 
DiscontinuedBB 
{BB 
getBB !
;BB! "
setBB# &
;BB& '
}BB( )
[DD 
BsonElementDD 
(DD 
$strDD "
)DD" #
]DD# $
publicEE 

stringEE 
QuantityPerUnitEE !
{EE" #
getEE$ '
;EE' (
setEE) ,
;EE, -
}EE. /
[GG 

BsonIgnoreGG 
]GG 
publicHH 

ListHH 
<HH  
MongoProductCategoryHH $
>HH$ %
ProductGenresHH& 3
{II 
getJJ 
{KK 	
returnMM 
[MM 
newMM 
(MM 
)MM 
{MM 
	ProductIdMM %
=MM& '
GuidHelpersMM( 3
.MM3 4
	IntToGuidMM4 =
(MM= >
	ProductIdMM> G
)MMG H
,MMH I

CategoryIdMMJ T
=MMU V
GuidHelpersMMW b
.MMb c
	IntToGuidMMc l
(MMl m

CategoryIDMMm w
)MMw x
}MMy z
]MMz {
;MM{ |
}OO 	
}PP 
[RR 

BsonIgnoreRR 
]RR 
publicSS 

ListSS 
<SS  
MongoProductPlatformSS $
>SS$ %
ProductPlatformsSS& 6
{TT 
getUU 
{VV 	
returnXX 
[XX 
newXX 
(XX 
)XX 
{XX 
	ProductIdXX %
=XX& '
GuidHelpersXX( 3
.XX3 4
	IntToGuidXX4 =
(XX= >
	ProductIdXX> G
)XXG H
,XXH I

PlatformIdXXJ T
=XXU V
GuidXXW [
.XX[ \
EmptyXX\ a
}XXb c
]XXc d
;XXd e
}ZZ 	
}[[ 
}\\ ô
sD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoProductCategory.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class  
MongoProductCategory !
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

Guid 

CategoryId 
{ 
get  
;  !
set" %
;% &
}' (
} ô
sD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoProductPlatform.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class  
MongoProductPlatform !
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

Guid 

PlatformId 
{ 
get  
;  !
set" %
;% &
}' (
} â
mD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoPublisher.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class 
MongoPublisher 
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
string 
CompanyName 
{ 
get  #
;# $
set% (
;( )
}* +
} æ
kD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoShipper.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
public 
class 
MongoShipper 
{ 
[ 
BsonId 
] 
[		 
BsonRepresentation		 
(		 
BsonType		  
.		  !
ObjectId		! )
)		) *
]		* +
public

 

string

 
ObjectId

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 
	ShipperID 
{ 
get 
; 
set  #
;# $
}% &
[ 
BsonElement 
( 
$str 
) 
]  
public 

string 
CompanyName 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
) 
] 
public 

string 
Phone 
{ 
get 
; 
set "
;" #
}$ %
} ô
lD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Entities\MongoSupplier.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Entities$ ,
;, -
[ #
BsonIgnoreExtraElements 
] 
public 
class 
MongoSupplier 
{ 
[		 
BsonId		 
]		 
[

 
BsonRepresentation

 
(

 
BsonType

  
.

  !
ObjectId

! )
)

) *
]

* +
public 

string 
ObjectId 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

int 

SupplierID 
{ 
get 
;  
set! $
;$ %
}& '
[ 
BsonElement 
( 
$str 
) 
]  
public 

string 
CompanyName 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
) 
]  
public 

string 
ContactName 
{ 
get  #
;# $
set% (
;( )
}* +
[ 
BsonElement 
( 
$str 
)  
]  !
public 

string 
ContactTitle 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 
BsonElement 
( 
$str 
) 
] 
public 

dynamic 
Address 
{ 
get  
;  !
set" %
;% &
}' (
[ 
BsonElement 
( 
$str 
) 
] 
public 

dynamic 
City 
{ 
get 
; 
set "
;" #
}$ %
[ 
BsonElement 
( 
$str 
) 
] 
public   

string   
Region   
{   
get   
;   
set    #
;  # $
}  % &
["" 
BsonElement"" 
("" 
$str"" 
)"" 
]"" 
public## 

dynamic## 
Country## 
{## 
get##  
;##  !
set##" %
;##% &
}##' (
[%% 
BsonElement%% 
(%% 
$str%% 
)%% 
]%% 
public&& 

dynamic&& 
Phone&& 
{&& 
get&& 
;&& 
set&&  #
;&&# $
}&&% &
[(( 
BsonElement(( 
((( 
$str(( 
)(( 
](( 
public)) 

dynamic)) 
Fax)) 
{)) 
get)) 
;)) 
set)) !
;))! "
}))# $
[++ 
BsonElement++ 
(++ 
$str++ 
)++ 
]++ 
public,, 

string,, 
HomePage,, 
{,, 
get,,  
;,,  !
set,," %
;,,% &
},,' (
}-- ¶
iD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Helpers\GuidHelpers.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Helpers$ +
;+ ,
public 
static 
class 
GuidHelpers 
{ 
public 

static 
Guid 
	IntToGuid  
(  !
int! $
value% *
)* +
{ 
var 
	guidBytes 
= 
new 
byte  
[  !
$num! #
]# $
;$ %
BitConverter 
. 
GetBytes 
( 
value #
)# $
.$ %
CopyTo% +
(+ ,
	guidBytes, 5
,5 6
$num7 8
)8 9
;9 :
return		 
new		 
Guid		 
(		 
	guidBytes		 !
)		! "
;		" #
}

 
public 

static 
int 
	GuidToInt 
(  
Guid  $
value% *
)* +
{ 
var 
	guidBytes 
= 
value 
. 
ToByteArray )
() *
)* +
;+ ,
return 
BitConverter 
. 
ToInt32 #
(# $
	guidBytes$ -
,- .
$num/ 0
)0 1
;1 2
} 
} Ú
oD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Helpers\RepositoryHelpers.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Helpers$ +
;+ ,
internal 
static	 
class 
RepositoryHelpers '
{ 
internal 
const 
string 
IdField !
=" #
$str$ )
;) *
} ◊
tD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\ICategoryRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
ICategoryRepository $
{ 
Task 
< 	
List	 
< 
MongoCategory 
> 
> 
GetAllAsync )
() *
)* +
;+ ,
Task		 
<		 	
MongoCategory			 
>		 
GetById		 
(		  
int		  #
id		$ &
)		& '
;		' (
}

 
oD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\ILogRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
ILogRepository 
{ 
Task 
LogGame	 
( 
GameUpdateLogEntry #
entry$ )
)) *
;* +
Task		 
LogGame			 
(		 
GameAddLogEntry		  
entry		! &
)		& '
;		' (
Task 
LogGame	 
( 
GameDeleteLogEntry #
entry$ )
)) *
;* +
} Õ	
qD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\IMongoUnitOfWork.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
IMongoUnitOfWork !
{ 
IProductRepository 
ProductRepository (
{) *
get+ .
;. /
}0 1
ICategoryRepository 
CategoryRepository *
{+ ,
get- 0
;0 1
}2 3
ISupplierRepository		 
SupplierRepository		 *
{		+ ,
get		- 0
;		0 1
}		2 3
IShipperRepository 
ShipperRepository (
{) *
get+ .
;. /
}0 1
IOrderRepository 
OrderRepository $
{% &
get' *
;* +
}, -"
IOrderDetailRepository !
OrderDetailRepository 0
{1 2
get3 6
;6 7
}8 9
ILogRepository 
LogRepository  
{! "
get# &
;& '
}( )
} Ÿ
wD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\IOrderDetailRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface "
IOrderDetailRepository '
{ 
Task 
< 	
List	 
< 
MongoOrderDetail 
> 
>  
GetByOrderIdAsync! 2
(2 3
int3 6
id7 9
)9 :
;: ;
} –
qD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\IOrderRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
IOrderRepository !
{ 
Task 
< 	
List	 
< 

MongoOrder 
> 
> 
GetAllAsync &
(& '
)' (
;( )
Task		 
<		 	

MongoOrder			 
>		 
GetByIdAsync		 !
(		! "
int		" %
id		& (
)		( )
;		) *
}

 ï
sD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\IProductRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
IProductRepository #
{ 
Task 
< 	
List	 
< 
MongoProduct 
> 
> 
GetAllAsync (
(( )
)) *
;* +

IQueryable		 
<		 
MongoProduct		 
>		 "
GetProductsAsQueryable		 3
(		3 4
)		4 5
;		5 6
Task 
< 	
MongoProduct	 
> 
GetByNameAsync %
(% &
string& ,
key- 0
)0 1
;1 2
Task 
< 	
List	 
< 
MongoProduct 
> 
>  
GetBySupplierIdAsync 1
(1 2
int2 5

supplierID6 @
)@ A
;A B
Task 
< 	
List	 
< 
MongoProduct 
> 
>  
GetByCategoryIdAsync 1
(1 2
int2 5

categoryId6 @
)@ A
;A B
Task 
< 	
MongoProduct	 
> 
GetByIdAsync #
(# $
int$ '
id( *
)* +
;+ ,
} ÿ
sD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\IShipperRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
IShipperRepository #
{ 
Task 
< 	
List	 
< 
MongoShipper 
> 
> 
GetAllAsync (
(( )
)) *
;* +
Task		 
<		 	
MongoShipper			 
>		 
GetByIdAsync		 #
(		# $
int		$ '
id		( *
)		* +
;		+ ,
}

 ù
tD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Interfaces\ISupplierRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $

Interfaces$ .
;. /
public 
	interface 
ISupplierRepository $
{ 
Task 
< 	
List	 
< 
MongoSupplier 
> 
> 
GetAllAsync )
() *
)* +
;+ ,
Task		 
<		 	
MongoSupplier			 
>		 
GetByIdAsync		 $
(		$ %
int		% (
id		) +
)		+ ,
;		, -
Task 
< 	
MongoSupplier	 
> 
GetByNameAsync &
(& '
string' -
companyName. 9
)9 :
;: ;
} ı
nD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\LoggingModels\MongoGenre.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
LoggingModels$ 1
;1 2
public 
class 

MongoGenre 
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
}# $
public		 

string		 
?		 
ParentGenreId		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
=		/ 0
null		1 5
!		5 6
;		6 7
}

 å
qD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\LoggingModels\MongoPlatform.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
LoggingModels$ 1
;1 2
public 
class 
MongoPlatform 
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
Type 
{ 
get 
; 
set !
;! "
}# $
} ˇ
eD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\MongoUnitOfWork.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
;# $
public 
class 
MongoUnitOfWork 
( 
IProductRepository 
productRepository (
,( )
ICategoryRepository 
categoryRepository *
,* +
ISupplierRepository 
supplierRepository *
,* +
IShipperRepository		 
shipperRepository		 (
,		( )
IOrderRepository

 
orderRepository

 $
,

$ %"
IOrderDetailRepository !
orderDetailRepository 0
,0 1
ILogRepository 
logRepository  
)  !
:" #
IMongoUnitOfWork$ 4
{ 
public 

IProductRepository 
ProductRepository /
=>0 2
productRepository3 D
;D E
public 

ICategoryRepository 
CategoryRepository 1
=>2 4
categoryRepository5 G
;G H
public 

ISupplierRepository 
SupplierRepository 1
=>2 4
supplierRepository5 G
;G H
public 

IShipperRepository 
ShipperRepository /
=>0 2
shipperRepository3 D
;D E
public 

IOrderRepository 
OrderRepository +
=>, .
orderRepository/ >
;> ?
public 
"
IOrderDetailRepository !!
OrderDetailRepository" 7
=>8 :!
orderDetailRepository; P
;P Q
public 

ILogRepository 
LogRepository '
=>( *
logRepository+ 8
;8 9
} ı
uD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\CategoryRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
public 
class 
CategoryRepository 
(  
IMongoDatabase  .
database/ 7
)7 8
:9 :
ICategoryRepository; N
{ 
private		 
const		 
string		 
CollectionName		 '
=		( )
$str		* 6
;		6 7
private 
readonly 
IMongoCollection %
<% &
MongoCategory& 3
>3 4
_collection5 @
=A B
databaseC K
.K L
GetCollectionL Y
<Y Z
MongoCategoryZ g
>g h
(h i
CollectionNamei w
)w x
;x y
public 

Task 
< 
List 
< 
MongoCategory "
>" #
># $
GetAllAsync% 0
(0 1
)1 2
{ 
var 
category 
= 
_collection "
." #
Find# '
(' (
_( )
=>* ,
true- 1
)1 2
.2 3
ToListAsync3 >
(> ?
)? @
;@ A
return 
category 
; 
} 
public 

Task 
< 
MongoCategory 
> 
GetById &
(& '
int' *
id+ -
)- .
{ 
var 
category 
= 
_collection "
." #
Find# '
(' (
x( )
=>* ,
x- .
.. /

CategoryId/ 9
==: <
id= ?
)? @
.@ A
FirstOrDefaultAsyncA T
(T U
)U V
;V W
return 
category 
; 
} 
} …
pD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\LogRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
public 
class 
LogRepository 
( 
IMongoDatabase )
database* 2
)2 3
:4 5
ILogRepository6 D
{ 
private		 
const		 
string		 
LogsCollectionName		 +
=		, -
$str		. 4
;		4 5
public 

async 
Task 
LogGame 
( 
GameUpdateLogEntry 0
entry1 6
)6 7
{ 
await &
EnsureLogsCollectionExists (
(( )
database) 1
)1 2
;2 3
var 

collection 
= 
database !
.! "
GetCollection" /
</ 0
GameUpdateLogEntry0 B
>B C
(C D
LogsCollectionNameD V
)V W
;W X
await 

collection 
. 
InsertOneAsync '
(' (
entry( -
)- .
;. /
} 
public 

async 
Task 
LogGame 
( 
GameDeleteLogEntry 0
entry1 6
)6 7
{ 
await &
EnsureLogsCollectionExists (
(( )
database) 1
)1 2
;2 3
var 

collection 
= 
database !
.! "
GetCollection" /
</ 0
GameDeleteLogEntry0 B
>B C
(C D
LogsCollectionNameD V
)V W
;W X
await 

collection 
. 
InsertOneAsync '
(' (
entry( -
)- .
;. /
} 
public 

async 
Task 
LogGame 
( 
GameAddLogEntry -
entry. 3
)3 4
{ 
await &
EnsureLogsCollectionExists (
(( )
database) 1
)1 2
;2 3
var 

collection 
= 
database !
.! "
GetCollection" /
</ 0
GameAddLogEntry0 ?
>? @
(@ A
LogsCollectionNameA S
)S T
;T U
await   

collection   
.   
InsertOneAsync   '
(  ' (
entry  ( -
)  - .
;  . /
}!! 
private## 
static## 
async## 
Task## &
EnsureLogsCollectionExists## 8
(##8 9
IMongoDatabase##9 G
database##H P
)##P Q
{$$ 
var%% 
collectionNames%% 
=%% 
await%% #
database%%$ ,
.%%, -
ListCollectionNames%%- @
(%%@ A
)%%A B
.%%B C
ToListAsync%%C N
(%%N O
)%%O P
;%%P Q
if&& 

(&& 
!&& 
collectionNames&& 
.&& 
Contains&& %
(&&% &
LogsCollectionName&&& 8
)&&8 9
)&&9 :
{'' 	
await(( 
database(( 
.(( !
CreateCollectionAsync(( 0
(((0 1
LogsCollectionName((1 C
)((C D
;((D E
})) 	
}** 
}++ Å
xD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\OrderDetailRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
internal 
class	 !
OrderDetailRepository $
($ %
IMongoDatabase% 3
database4 <
)< =
:> ?"
IOrderDetailRepository@ V
{ 
private 
const 
string 
CollectionName '
=( )
$str* 9
;9 :
private		 
readonly		 
IMongoCollection		 %
<		% &
MongoOrderDetail		& 6
>		6 7
_collection		8 C
=		D E
database		F N
.		N O
GetCollection		O \
<		\ ]
MongoOrderDetail		] m
>		m n
(		n o
CollectionName		o }
)		} ~
;		~ 
public 

Task 
< 
List 
< 
MongoOrderDetail %
>% &
>& '
GetByOrderIdAsync( 9
(9 :
int: =
id> @
)@ A
{ 
var 
order 
= 
_collection 
.  
Find  $
($ %
x% &
=>' )
x* +
.+ ,
OrderId, 3
==4 6
id7 9
)9 :
.: ;
ToListAsync; F
(F G
)G H
;H I
return 
order 
; 
} 
} ÿ
rD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\OrderRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
public 
class 
OrderRepository 
( 
IMongoDatabase +
database, 4
)4 5
:6 7
IOrderRepository8 H
{		 
private

 
const

 
string

 
CollectionName

 '
=

( )
$str

* 2
;

2 3
private 
readonly 
IMongoCollection %
<% &

MongoOrder& 0
>0 1
_collection2 =
=> ?
database@ H
.H I
GetCollectionI V
<V W

MongoOrderW a
>a b
(b c
CollectionNamec q
)q r
;r s
public 

Task 
< 
List 
< 

MongoOrder 
>  
>  !
GetAllAsync" -
(- .
). /
{ 
var 
orders 
= 
_collection  
.  !
Find! %
(% &
_& '
=>( *
true+ /
)/ 0
.0 1
ToListAsync1 <
(< =
)= >
;> ?
return 
orders 
; 
} 
public 

Task 
< 

MongoOrder 
> 
GetByIdAsync (
(( )
int) ,
id- /
)/ 0
{ 
var 
order 
= 
_collection 
.  
Find  $
($ %
x% &
=>' )
x* +
.+ ,
OrderId, 3
==4 6
id7 9
)9 :
.: ;
FirstOrDefaultAsync; N
(N O
)O P
;P Q
return 
order 
; 
} 
} ó$
tD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\ProductRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
public 
class 
ProductRepository 
( 
IMongoDatabase -
database. 6
)6 7
:8 9
IProductRepository: L
{ 
private		 
const		 
string		 
CollectionName		 '
=		( )
$str		* 4
;		4 5
private

 
readonly

 
IMongoCollection

 %
<

% &
MongoProduct

& 2
>

2 3
_collection

4 ?
=

@ A
database

B J
.

J K
GetCollection

K X
<

X Y
MongoProduct

Y e
>

e f
(

f g
CollectionName

g u
)

u v
;

v w
public 

Task 
< 
List 
< 
MongoProduct !
>! "
>" #
GetAllAsync$ /
(/ 0
)0 1
{ 
var 
products 
= 
_collection "
." #
Find# '
(' (
_( )
=>* ,
true- 1
)1 2
.2 3
ToListAsync3 >
(> ?
)? @
;@ A
return 
products 
; 
} 
public 

Task 
< 
MongoProduct 
> 
GetByIdAsync *
(* +
int+ .
id/ 1
)1 2
{ 
var 
product 
= 
_collection !
.! "
Find" &
(& '
x' (
=>) +
x, -
.- .
	ProductId. 7
==8 :
id; =
)= >
.> ?
FirstOrDefaultAsync? R
(R S
)S T
;T U
return 
product 
; 
} 
public 


IQueryable 
< 
MongoProduct "
>" #"
GetProductsAsQueryable$ :
(: ;
); <
{ 
return 
database 
. 
GetCollection %
<% &
MongoProduct& 2
>2 3
(3 4
$str4 >
)> ?
.? @
AsQueryable@ K
(K L
)L M
;M N
} 
public 

Task 
< 
MongoProduct 
> 
GetByNameAsync ,
(, -
string- 3
key4 7
)7 8
{ 
var 
products 
= 
_collection "
." #
Find# '
(' (
x( )
=>* ,
x- .
.. /
ProductName/ :
==; =
key> A
)A B
.B C
FirstOrDefaultAsyncC V
(V W
)W X
;X Y
return   
products   
;   
}!! 
public## 

Task## 
<## 
List## 
<## 
MongoProduct## !
>##! "
>##" # 
GetBySupplierIdAsync##$ 8
(##8 9
int##9 <

supplierID##= G
)##G H
{$$ 
var%% 
products%% 
=%% 
_collection%% "
.%%" #
Find%%# '
(%%' (
x%%( )
=>%%* ,
x%%- .
.%%. /

SupplierID%%/ 9
==%%: <

supplierID%%= G
)%%G H
.%%H I
ToListAsync%%I T
(%%T U
)%%U V
;%%V W
return&& 
products&& 
;&& 
}'' 
public)) 

Task)) 
<)) 
List)) 
<)) 
MongoProduct)) !
>))! "
>))" # 
GetByCategoryIdAsync))$ 8
())8 9
int))9 <

categoryId))= G
)))G H
{** 
var++ 
products++ 
=++ 
_collection++ "
.++" #
Find++# '
(++' (
x++( )
=>++* ,
x++- .
.++. /

CategoryID++/ 9
==++: <

categoryId++= G
)++G H
.++H I
ToListAsync++I T
(++T U
)++U V
;++V W
return,, 
products,, 
;,, 
}-- 
}.. º
tD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\ShipperRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
public 
class 
ShipperRepository 
( 
IMongoDatabase -
database. 6
)6 7
:8 9
IShipperRepository: L
{ 
private		 
const		 
string		 
CollectionName		 '
=		( )
$str		* 4
;		4 5
private

 
readonly

 
IMongoCollection

 %
<

% &
MongoShipper

& 2
>

2 3
_collection

4 ?
=

@ A
database

B J
.

J K
GetCollection

K X
<

X Y
MongoShipper

Y e
>

e f
(

f g
CollectionName

g u
)

u v
;

v w
public 

async 
Task 
< 
List 
< 
MongoShipper '
>' (
>( )
GetAllAsync* 5
(5 6
)6 7
{ 
var 
shippers 
= 
await 
_collection (
.( )
Find) -
(- .
_. /
=>0 2
true3 7
)7 8
.8 9
ToListAsync9 D
(D E
)E F
;F G
return 
shippers 
; 
} 
public 

async 
Task 
< 
MongoShipper "
>" #
GetByIdAsync$ 0
(0 1
int1 4
id5 7
)7 8
{ 
var 
shipper 
= 
await 
_collection '
.' (
Find( ,
(, -
x- .
=>/ 1
x2 3
.3 4
	ShipperID4 =
==> @
idA C
)C D
.D E
FirstOrDefaultAsyncE X
(X Y
)Y Z
;Z [
return 
shipper 
; 
} 
} Û
uD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Repositories\SupplierRepository.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
Repositories$ 0
;0 1
public 
class 
SupplierRepository 
(  
IMongoDatabase  .
database/ 7
)7 8
:9 :
ISupplierRepository; N
{ 
private		 
const		 
string		 
CollectionName		 '
=		( )
$str		* 5
;		5 6
private

 
readonly

 
IMongoCollection

 %
<

% &
MongoSupplier

& 3
>

3 4
_collection

5 @
=

A B
database

C K
.

K L
GetCollection

L Y
<

Y Z
MongoSupplier

Z g
>

g h
(

h i
CollectionName

i w
)

w x
;

x y
public 

async 
Task 
< 
List 
< 
MongoSupplier (
>( )
>) *
GetAllAsync+ 6
(6 7
)7 8
{ 
var 
supplier 
= 
await 
_collection (
.( )
Find) -
(- .
_. /
=>0 2
true3 7
)7 8
.8 9
ToListAsync9 D
(D E
)E F
;F G
return 
supplier 
; 
} 
public 

async 
Task 
< 
MongoSupplier #
># $
GetByIdAsync% 1
(1 2
int2 5
id6 8
)8 9
{ 
var 
supplier 
= 
await 
_collection (
.( )
Find) -
(- .
x. /
=>0 2
x3 4
.4 5

SupplierID5 ?
==@ B
idC E
)E F
.F G
FirstOrDefaultAsyncG Z
(Z [
)[ \
;\ ]
return 
supplier 
; 
} 
public 

async 
Task 
< 
MongoSupplier #
># $
GetByNameAsync% 3
(3 4
string4 :
companyName; F
)F G
{ 
var 
supplier 
= 
await 
_collection (
.( )
Find) -
(- .
x. /
=>0 2
x3 4
.4 5
CompanyName5 @
==A C
companyNameD O
)O P
.P Q
FirstOrDefaultAsyncQ d
(d e
)e f
;f g
return 
supplier 
; 
} 
} •
sD:\OneDrive\Dokumenty\Programowanie\EPAM\Repos\Gamestore\Gamestore.MongoRepository\Settings\MongoDBSettingsModel.cs
	namespace 	
	Gamestore
 
. 
MongoRepository #
.# $
MongoDB$ +
;+ ,
public 
class  
MongoDBSettingsModel !
{ 
public 

string 
ConnectionString "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 

string 
DatabaseName 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 