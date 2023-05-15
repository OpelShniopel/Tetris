Tetriai
Komandos nariai (IFIN-1/3):
Martynas Jakovlevas
Ugnė Vasiliauskaitė
Dovydas Šiurkus
Martynas Karpinas
Esame aistringi programuotojai ir žaidimų entuziastai. Mūsų tikslas buvo sukurti žaidimą, kuris būtų malonus ir iššūkį teikiantis žaidėjams visame pasaulyje.
Techninė užduotis
Sukurti „Tetris“ žaidimą, tačiau implementuoti naujų ypatumų, bei dizaino skirtumų.
Aprašymas
Pamirškite apie įprastą žaidimą, pasirinkite naująją žaidimo patirtį - ,,Tetris’’! ,,Tetris’’  yra klasikinis blokų sudėliojimo žaidimas, kuriame jūsų tikslas yra sudėlioti blokus taip, kad užpildytumėte horizontalias eilutes ir gautumėte taškus. Tačiau mes nekalbame apie standartinį ,,Tetris’’, mes siūlome visiškai naują, inovatyvią žaidimo patirtį, atsisiųskite ir įsitikinsite patys!
Funkcionalumai
Klasikinis ,,Tetrio’’ žaidimo modelis: žaidimas seka originalaus ,,Tetrio’’ žaidimo principus - sukurti pilnas eilutes naudojant įvairių formų blokus ir gauti taškus.
Intuityvi ir paprasta vartotojo sąsaja: žaidimas yra suprojektuotas taip, kad jis būtų patogus ir lengvai naudojamas visiems vartotojams, nepriklausomai nuo jų patirties su kompiuteriniais žaidimais.
Taškų sistema: žaidime įdiegta taškų sistema, leidžianti stebėti savo pažangą ir lyginti savo rezultatus su ankstesniais.
Keletas lygių sunkumo: žaidime yra keli sunkumo lygiai, pradedant nuo lengvo ir baigiant sunkiu lygiu, kuris yra tikras iššūkis net patyrusiems ,,Tetris’’ žaidėjams.
Galimybė saugoti ir atnaujinti savo geriausius rezultatus: žaidimas leidžia išsaugoti jūsų geriausius rezultatus, kad galėtumėte stebėti savo pažangą ir siekti naujų rekordų.
Keletas žaidimo režimų: žaidime yra trys režimai: "trijų gyvybių" režimas, kuriame turite tik tris gyvybes; "paprastas" režimas, kuriame galite žaisti be jokių apribojimų; ir "be pabaigos" režimas, kuris tęsiasi tol, kol nusprendžiate jį baigti.

Kaip žaisti
Turbūt jau žinote kaip žaisti klasikinį žaidimą Tetrį, todėl turėtumėte žinoti kaip žaisti ir mūsų Tetrį. Tačiau valdymai gali skirtis nuo įprasto Tetrio, todėl greitai pristatysiu valdymą klaviatūra:
,,a” - stumteli figūrėlę į kairę;
,,d” - stumteli figūrėlę į dešinę; 
,,s” - stumteli figūrėlę žemyn;
,,q” - pasuka figūrėlę 90 laipsnių;
,,e” - pasuka figūrėlę -90 laipsnių;
,,space” - iš karto numeta figūrėlę iki žemės.
Tetrį nėra sudėtinga suprasti kaip žaisti - atsitiktinai parinktos figūrėlės krenta vis greičiau einant laikui. Šiomis figūrėlėmis turite užpildyti pilną liniją, kad sunaikintumėte liniją. Sunaikinus liniją gaunami taškai. Jeigu figūrėlė pasiekia žaidimo lauko viršų - pralaimima. Žaidimo tikslas – surinkti kuo daugiau daugiau taškų. Tačiau šis standartinis rėžimas nėra vienintelis, nes jis gali greitai pabosti, todėl sukūrėme dar du žaidimo rėžimus:
,,3 gyvybių” rėžimas. Kuom šis rėžimas skiriasi nuo standartinio, tai kad kai standartiniame rėžime kai pralaimima, žaidimas pasibaigia. ,,3 gyvybių” rėžime prarandama gyvybė, o kai prarandamos visos trys – tik tada žaidimas pasibaigia.
,,Be pabaigos” rėžimas. Kuom šis režimas skiriasi nuo ,,3 gyvybių” rėžimo, tai kad turime begalybę gyvybių.
Diegimo instrukcijos
Mūsų projekto „Github“ rasite šioje nuorodoje: https://github.com/OpelShniopel/Tetris
Jos pagalba, galite atsisiųsti žaidimą iš svetainės, pasidėti pas save kompiuteryje. Tuomet Jums reikės atsisiųsti programėlę „Unity“, kurios pagalba Jūs žaidimą galėsite paleisti savo kompiuteryje. Atsidarę projekto aplanką su „Unity“, matysite pagrindinį žaidimo ekraną. Tačiau pirma reikia paspausti viršuje play mygtuką, ir žaidimą galite pradėti.
Šis žaidimas yra tinkamas įvairaus amžiaus asmenims. Sėkmingai remiantis šiuo projekto aprašymu, instrukcijomis, bus tinkamai žaidžiama. Smagaus žaidimo!
Architektūra
Pieces aplankalas:
•	GhostPiece – ši klasė sukuria krentančios figūrėlės „vaiduoklį“, kurio pagalba žaidėjas gali pamatyti, kur nukris figūrėlė lentelėje.
•	Piece – viena svarbiausių klasių. Ji valdo figūrėlių kritimą, greitį, sukimąsi, judėjimą, žaidėjo klavišų spaudimus, vyksta realiam laike.
•	Tetromino – sukuria figūrėlių formas, aprašo jų dydį, kiekvienos figūrėlės sąveika su lentelės sienomis.


Audio aplankalas:
•	AudioManager – objektas, kuris yra atsakingas už muziką bei garsą.
Utilities aplankalas:
•	ChangeScene – objektas, kuris užkrauną sceną.
•	Data – objektas, kuriame yra saugoma figūrėlių sandara bei figūrėlių sąveika tarpusavyje, su žaidimo lauku ir kaip turima pasukti figūrėles.
User Interface (UI) - šis projektas yra atsakingas už:
•	HealthBar – gyvybių režimo grafinis atvaizdavimas. Šiam veiksmui yra sukurta klasė kurioje yra keturi metodai, kurie atlieka gyvybės padalinimą, suklydimo atveju (iš viso galima suklysti tris kartus).
•	PauseMenu – klasė atsakinga už pauzės tinkamą veikimą, žaidimo tęsimą, grįžimą atgal į pagrindinį meniu.
Core aplankalas (pagrindinė žaidimo logika):
•	Board klasė – tai yra atsakinga už blokų tvarkymą žaidimo lentoje. Tai apima blokų judėjimą, linijų pašalinimą, kai jos yra pilnai užpildytos.
•	Diffuculty enum – enumeracija, kurioje saugomi žaidimo sunkumo lygiai (lengvas, vidutinis, sunkus).
•	DifficultyManager klasė – atsakinga už sunkumo lygio keitimą pagal vartotojo pasirinkimą ir žaidimo progresą. Ši klasė naudoja enum reikšmes ir keičia blokų judėjimo greitį.
•	ScoreManager klasė – ši klasė tvarkosi su taškais. Ji gali pridėti taškus, kai žaidėjas užbaigia liniją ir atnaujinto geriausio rezultato saugojimo sistemą, kai žaidėjas pasiekia nauja rekordą.
•	GameManager klasė – ši klasė tvarkosi su skirtingais žaidimo režimais pagal vartotojo pasirinkimą (trijų gyvybių, paprastu, be pabaigos).

Testavimas
Trijų gyvybių režimas: šio testo metu bus tikrinama ar šis režimas tinkamai veikia, t.y ar gyvybės atitinkamai mažėja po kiekvieno pilnos lentelės pripildymo. Prisipildžius visai lentelei, pirmiausia yra atimama ekrane matoma širdelės dalis (trečdalis). Širdelė – žaidėjo gyvybių indikatorius. Suklydus darsyk, jau matoma likusi tik trečdalis širdelės, kas reiškia, jog liko paskutinis bandymas. Suklydus paskutinįjį kartą, širdelė tampa juoda ir ekrane atsiranda ekranas su žinute: „Žaidimas baigtas“. Šis režimas veikia tinkamai, klaidų neatsirado, gyvybių sistema veikia tinkamai, pauzė veikia, grįžimas į meniu po pralaimėjimo taip pat veikia.
Begalinis: atlikus šio rėžimo testą, įvertinta, kad per nustatytą laiko tarpą žaidimas tęsiasi, o pralaimėjimas negalimas (užpildžius figūrėlėmis žaidimo lauką iki pat viršaus ekranas išsivalo ir žaidimas tęsiasi).
Standartinis: Šio testo metu bus tikrinama ar programa veikia tinkamai, tai yra ar sukuriama žaidimo lenta tinkamoje vietoje, tinkamo dydžio, sukuriamos figūrėlės. Šį testą atlikome paleisdami standartinį rėžimą ir bandydami figūrėles judinti kaip turėtų judėti teoriškai, tai yra figūros neišeina iš lentos ribų. Šį testas buvo įvykdytas sėkmingai.
Muzika: mūsų žaidimas yra papildytas ne viena muzika. Kiekvienas režimas turi savo atitinkamą muziką. Pagrindinis meniu taip pat turi savo atskirą muziką. Pradėjus kokį nors režimą, pagrindinė muzika išsijungia, ir yra įjungiama atitinkama režimo muzika. Visų režimų muzika veikia atitinkamai. Grįžus atgal į pagrindinį meniu, režimo muzika yra sustabdoma ir grojama pagrindinė muzika. Taip pat, žaidėjui pralaimėjus standartiniame ir begaliniame režimuose yra grojamas dar vienas garso efektas, kuris išsijungia pasibaigus jam ar grįžus atgal į pagrindinį meniu.
Rėžimai bei muzika veikia tinkamai. Pereikime prie testų:
Judėjimo testas: šis testas patikrina „Tetrio“ figūrėlių elgsena. Ar kai atliekamas judėjimas figūra juda kaip tikimasi. Ar figūrai neleidžiama judėti už lentos ribų.
Žaidimo pabaigos testas: testuojama ar žaidimas baigiasi, kai figūros pasiekia lentos viršų.
Lygio progresijos testas: šis testas patikrina ar teisingai veikia 
Sunkumo Lygių Testai: šis testas parodo ar žaidimas prasideda nustatytų sunkumo lygiu ir sunkumas didėja pagal surinktų taškų skaičių.
Rezultatų Saugojimo Testai: 
•	Testas, ar geriausi rezultatai teisingai išsaugomi.
•	Testas, ar rezultatai atnaujinami, kai pasiekiamas naujas rekordas.
•	Testas, ar rezultatai yra saugomi tarp skirtingų žaidimo sesijų.
Taškų Sistemos Testai: 
•	Testas, ar taškai teisingai skaičiuojami, kai užpildoma eilutė.
•	Testas, ar taškai teisingai skaičiuojami, kai užpildomos kelios eilutės iš karto.
Visi šie testai buvo atlikti, ir jų metu buvo tiriamas žaidimo funkcionalumas. Galime teigti, jog programa veikia be klaidų, kadangi jų neužtikome, bei viską ką testavome grąžino teigiamą rezultatą.
