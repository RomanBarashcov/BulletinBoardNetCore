﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.EF
{
    public class SeedData
    {
        public static class ProductType
        {
            public const string iPhone = "iPhone";
            public const string iPad = "iPad";
            public const string iMacAndMacbook = "iMac & Macbook";
            public const string iPod = "iPod";
            public const string AppleWatch = "Apple Watch";
            public const string AppleTV = "Apple TV";
            public const string Accessories = "Accessories";
            public const string Other = "Other";
        }

        public static class iPhones
        {
            public const string iPhone2G = "iPhone 2G";
            public const string iPhone3G = "iPhone 3G";
            public const string iPhone3GS = "iPhone 3GS";
            public const string iPhone4 = "iPhone 4";
            public const string iPhone4S = "iPhone 4S";
            public const string iPhone5 = "iPhone 5";
            public const string iPhone5C = "iPhone 5C";
            public const string iPhone5S = "iPhone 5S";
            public const string iPhoneSE = "iPhone SE";
            public const string iPhone6 = "iPhone 6";
            public const string iPhone6Plus = "iPhone 6 Plus";
            public const string iPhone6S = "iPhone 6S";
            public const string iPhone6SPlus = "iPhone 6S Plus";
            public const string iPhone7 = "iPhone 7";
            public const string iPhone7Plus = "iPhone 7 Plus";
            public const string iPhone8 = "iPhone 8";
            public const string iPhone8Plus = "iPhone 8 Plus";
            public const string iPhoneX = "iPhone X";
        }

        public static class Macs
        {
            public const string MacBook = "MacBook";
            public const string MacBookPro = "MacBook Pro";
            public const string MacBookAir = "MacBook Air";
            public const string iMacMini = "iMac Mini";
            public const string iMac = "iMac";
            public const string MacPro = "Mac Pro";
        }

        public static class iPads
        {
            public const string iPad = "iPad";
            public const string iPad2 = "iPad 2";
            public const string iPad3 = "iPad 3";
            public const string iPad4 = "iPad 4";
            public const string iPadAir = "iPad Air";
            public const string iPadAir2 = "iPad Air 2";
            public const string iPadMini = "iPad Mini";
            public const string iPadMini2 = "iPad Mini 2";
            public const string iPadMini3 = "iPad Mini 3";
            public const string iPadMini4 = "iPad Mini 4";
            public const string iPadPro9_7 = "iPad Pro 9.7";
            public const string iPadPro10_5 = "iPad Pro 10.5";
            public const string iPad2017 = "iPad 2017";
            public const string iPad2018 = "iPad 2018";
        }

        public static class iPods
        {
            public const string iPodShuffle = "iPod Shuffle";
            public const string iPodNano = "iPod Nano";
            public const string iPodClassic = "iPod Classic";
            public const string iPodTouch = "iPod Touch";
        }

        public static class AppleWatches
        {
            public const string AppleWatch = "Apple Watch";
            public const string AppleWatchSport = "Apple Watch Sport";
            public const string AppleWatchEdition = "Apple Watch Edition";
            public const string Series1 = "Series 1";
            public const string Series1Sport = "Series 1 Sport";
            public const string Series2 = "Series 2";
            public const string Series2Sport = "Series 2 Sport";
            public const string Series3 = "Series 3";
            public const string Series3Sport = "Series 3 Sport";
        }

        public static class AppleTVs
        {
            public const string AppleTV = "Apple TV";
            public const string AppleTV2 = "Apple TV 2";
            public const string AppleTV3 = "Apple TV 3";
            public const string AppleTV4 = "Apple TV 4";
            public const string AppleTV4K = "Apple TV 4K";
        }


        public static class States
        {
            public const string New = "Новый";
            public const string LikeNew = "Как новый";
            public const string SecondHand = "Б/У";
            public const string ForParts = "На запчасти";
        }

        public static class Colors
        {
            public const string SpaceGrey = "Space Grey";
            public const string Silver = "Silver";
            public const string Gold = "Gold";
            public const string RoseGold = "Rose Gold";
            public const string White = "White";
            public const string Black = "Black";
            public const string JetBlack = "Jet Black";
        }

        public static class Memmories
        {
            public const int _4GB = 4;
            public const int _8GB = 8;
            public const int _16GB = 16;
            public const int _32GB = 32;
            public const int _64GB = 64;
            public const int _128GB = 128;
            public const int _256GB = 256;
            public const int _512GB = 512;
            public const int _1TB = 1;
            public const int _2TB = 2;
        }

        public static class CityAreas
        {
            public const string Vineckaya = "Винницкая";
            public const string Volinskaya = "Волынская";
            public const string Dnepropetrovskaya = "Днепропетровская";
            public const string Donetsk = "Донецкая";
            public const string Zhitomirskaya ="Житомирская";
            public const string Zakarpatskaya = "Закарпатская";
            public const string Zaporozhskaya = "Запорожская";
            public const string IvanoFrankovskaya = "Ивано-Франковская";
            public const string Kievskaya = "Киевская";
            public const string Kirovogradskays = "Кировоградская";
            public const string Liganskaya = "Луганская";
            public const string Lvovskaya = "Львовская";
            public const string Nikolaevskaya = "Николаевская";
            public const string Odesskaya = "Одесская";
            public const string Poltavskaya = "Полтавская";
            public const string Rovnenskaya = "Ровенская";
            public const string Sumskaya = "Сумская";
            public const string Ternopolskaya = "Тернопольская";
            public const string Kharkovskaya = "Харьковская";
            public const string Khersonskaya = "Херсонская";
            public const string Khmelnickaya = "Хмельницкая";
            public const string Cherkaskaya = "Черкасская";
            public const string Chernigovskaya = "Черниговская";
            public const string Chernoveckaya = "Черновицкая";
        }

        public static class VineckayaCities
        {
            public const string Bar = "Бар";
            public const string Berdash = "Бершадь";
            public const string Vinniciya = "Винница";
            public const string Gaisin = "Гайсин";
            public const string Gnivan = "Гнивань";
            public const string Gmerinka = "Жмеринка";
            public const string Ilyicin = "Ильинцы";
            public const string Kazatin = "Казатин";
            public const string Kalinovka = "Калиновка";
            public const string Krijopol = "Крыжополь";
            public const string Ladijin = "Ладыжин";
            public const string Lipovec = "Липовец";
            public const string MaghilevoPodolsk = "Магилево-Подольский";
            public const string Nemirov = "Немиров";
            public const string Pesochin = "Песочин";
            public const string Pogrebishe = "Погребище";
            public const string Strijovka = "Стрижовка";
            public const string Tulchin = "Тульчин";
            public const string Khmelnik = "Хмельник";
            public const string Chechelnik = "Чечельник";
            public const string Shargorod = "Шаргород";
            public const string Yampol = "Ямполь";
        }

        public static class VolinskayaCities
        {
            public const string Berestechko = "Берестечко";
            public const string VladimirVolinsky = "Владимир-Волынский";
            public const string Gorohov = "Горохов";
            public const string Ivanovich = "Иваничи";
            public const string KamenKashirskiy = "Камень-Каширский";
            public const string Kiverciy = "Киверцы";
            public const string Kovel = "Ковель";
            public const string Luck = "Луцк";
            public const string Lubeshov = "Любешов";
            public const string Lubomol = "Любомль";
            public const string Mavich = "Маневичи";
            public const string Novovolinsk = "Нововолынск";
            public const string Ratno = "Ратно";
            public const string Rojishe = "Рожище";
            public const string StarayaVijevka= "Старая Выжевка";
            public const string Turiysk = "Турийск";
            public const string Ystylyg = "Устилуг";
            public const string Cuman = "Цумань";
            public const string Shacak = "Шацк";
        }

        public static class DnepropetrovskCities
        {
            public const string Apostolovo = "Апостолово";
            public const string Verhnedneprovsk = "Верхнеднепровск";
            public const string Volnyogorsk = "Вольногорск";
            public const string Dnepr = "Днепр";
            public const string JeltieVodi = "Желтые Воды";
            public const string Kamenskoe = "Каменское";
            public const string KrivoyRog = "Кривой Рог";
            public const string Marganec = "Марганец";
            public const string Nikopol = "Никополь";
            public const string Novomoskovsk = "Новомосковск";
            public const string Orjenihize = "Орджоникидзе";
            public const string Pavlograd = "Павлоград";
            public const string Pereshepino = "Перещепино";
            public const string Pershotravensk = "Першотравенск";
            public const string Podgorodnoe = "Подгородное";
            public const string Patihatki = "Пятихатки";
            public const string Sinelnikovo = "Синельниково";
            public const string Ternovka = "Терновка";
            public const string Chaplinka = "Чаплинка";
        }

        public static class DonetskCities
        {
            public const string Avdeevka = "Авдеевка";
            public const string Alexandrovka = "Александровка";
            public const string Amvroisievka = "Амвросиевка";
            public const string Artemovsk = "Артемовск";
            public const string Volnovaha = "Волноваха";
            public const string Gorlovka = "Горловка";
            public const string Debalcevo = "Дебальцево";
            public const string Djerzinsk = "Дзержинск";
            public const string Demitrov = "Димитров";
            public const string Dobropolie = "Доброполье";
            public const string Dokcuchaevsk = "Докучаевск";
            public const string Doneck  = "Донецк";
            public const string Drujovka = "Дружковка";
            public const string Enakievo = "Енакиево";
            public const string Jdanovka = "Ждановка";
            public const string Zugres = "Зугрэс";
            public const string Kirovskoe = "Кировское";
            public const string Konstantinovka = "Константиновка";
            public const string Kramatorsk = "Краматорск";
            public const string Krasnoarmeysk = "Красноармейск";
            public const string KrasniyLiman = "Красный Лиман";
            public const string Mayorsk = "Майорск";
            public const string Makeevka = "Макеевка";
            public const string Mariupol = "Мариуполь";
            public const string Marinka = "Марьинка";
            public const string Novoazonsk = "Новоазовск";
            public const string Novogorodovka = "Новогродовка";
            public const string Selidovo = "Селидово";
            public const string Slavansk = "Славянск";
            public const string Snejnoye = "Снежное";
            public const string Soledar = "Соледар";
            public const string Starobeshovo = "Старобешево";
            public const string Torez = "Торез";
            public const string Ugleder = "Угледар";
            public const string Kharzisk = "Харцызск";
            public const string Shahtersk = "Шахтерск";
            public const string Yasinovataya = "Ясиноватая";
        }

        public static class ZhitomirskCities
        {
            public const string Andrushevka = "Андрушевка";
            public const string Baranovka = "Барановка";
            public const string Berdichev = "Бердичев";
            public const string VolodarskVolinsk = "Володарск-Волынский";
            public const string Emilchino = "Емильчино";
            public const string Djitomir = "Житомир";
            public const string Irshanskiy = "Иршанск";
            public const string Korosten = "Коростень";
            public const string Korostishev = "Коростышев";
            public const string Malin = "Малин";
            public const string NovogradVolinsk = "Новоград-Волынский";
            public const string Ovrush = "Овруч";
            public const string Olevsk = "Олевск";
            public const string Popelnya = "Попельня";
            public const string Rodomishl = "Радомышль";
            public const string Romanov = "Романов";
            public const string Chernyahov = "Черняхов";
        }

        public static class ZakarpatskCities
        {
            public const string Beregovo = "Берегово";
            public const string Bushtinova = "Буштына";
            public const string VelikiyBichkov = "Великий Бычков";
            public const string Vinogradov = "Виноградов";
            public const string Vishkovo = "Вышково";
            public const string Dybovoe = "Дубовое";
            public const string Irshava = "Иршава";
            public const string Korolevo = "Королево";
            public const string Mejgorie = "Межгорье";
            public const string Mukachevo = "Мукачево";
            public const string Perechin = "Перечин";
            public const string Rahov = "Рахов";
            public const string Svalyava = "Свалява";
            public const string Solotvina = "Солотвина";
            public const string Tyachev = "Тячев";
            public const string Ujgorod = "Ужгород";
            public const string Hust = "Хуст";
            public const string Chop = "Чоп";
        }

        public static class ZaporozhieCities
        {
            public const string Akimovka = "Акимовка";
            public const string Belyaevka = "Беляевка";
            public const string Berdansk = "Бердянск";
            public const string Vasilievka = "Васильевка";
            public const string Vesoloe= "Веселое";
            public const string Volnansk = "Вольнянск";
            public const string GulayPole = "Гуляйполе";
            public const string Dneprorudnoe = "Днепрорудное";
            public const string Zaporojie = "Запорожье";
            public const string KamenkaDneprovskaya = "Каменка-Днепровская";
            public const string Kuibishevo = "Куйбышево";
            public const string Kushugum = "Кушугум";
            public const string Melitopol = "Мелитополь";
            public const string Mihailovka = "Михайловка";
            public const string Molochansk = "Молочанск";
            public const string Orehov = "Орехов";
            public const string Pologin = "Пологи";
            public const string Primorsk = "Приморск";
            public const string Rozovka = "Розовка";
            public const string Tokmak = "Токмак";
            public const string Energodar = "Энергодар";
        }

        public static class IvanoFrankovskCities
        {
            public const string Bogorchani = "Богородчаны";
            public const string Bolehov = "Болехов";
            public const string Burshtin = "Бурштын";
            public const string Galich = "Галич";
            public const string Gorodenka = "Городенка";
            public const string Delyatin = "Делятин";
            public const string Dolina = "Долина";
            public const string IvanoFrankovsk = "Ивано-Франковск";
            public const string Kalush = "Калуш";
            public const string Kolomiya = "Коломыя";
            public const string Kosov = "Косов";
            public const string Lanchin = "Ланчин";
            public const string Nadvornaya = "Надворная";
            public const string Pereginskoye = "Перегинское";
            public const string Rogatin = "Рогатин";
            public const string Snyatin = "Снятын";
            public const string Tlymach = "Тлумач";
            public const string Tismenica = "Тысменица";
            public const string Yaremche = "Яремче";
        }

        public static class KievCities
        {
            public const string Barishevka = "Барышевка";
            public const string BelayaCerkov = "Белая Церковь";
            public const string Berezan = "Березань";
            public const string Boguslav = "Богуслав";
            public const string Borispol = "Борисполь";
            public const string Borodyanka = "Бородянка";
            public const string Boyarka = "Боярка";
            public const string Brovari = "Бровары";
            public const string Bucha = "Буча";
            public const string Vasilkov = "Васильков";
            public const string Vishnevoe = "Вишневое";
            public const string Volodarka = "Володарка";
            public const string Vishgorod = "Вышгород";
            public const string Glevaha = "Глеваха";
            public const string Gostomel = "Гостомель";
            public const string Ivankov = "Иванков";
            public const string Irpen = "Ирпень";
            public const string Kagarlik = "Кагарлык";
            public const string Kiyev = "Киев";
            public const string Kocybinskoe = "Коцюбинское";
            public const string Makarov = "Макаров";
            public const string Mironovka = "Мироновка";
            public const string Obyhov = "Обухов";
            public const string PereyaslovHmelnitskiy = "Переяслав-Хмельницкий";
            public const string Pripyat = "Припять";
            public const string Rjishev = "Ржищев";
            public const string Rokitnoe = "Рокитное";
            public const string Skvira = "Сквира";
            public const string Slavytich = "Славутич";
            public const string Tarasha = "Тараща";
            public const string Teterev = "Тетиев";
            public const string Uzin = "Узин";
            public const string Ukrainka = "Украинка";
            public const string Fastov = "Фастов";
            public const string Chernobyil = "Чернобыль";
            public const string Yagotin = "Яготин";
        }

        public static class KirovogradCities
        {
            public const string Alexandiya = "Александрия";
            public const string Bobrinec = "Бобринец";
            public const string Vlasovka = "Власовка";
            public const string Gayvoron = "Гайворон";
            public const string Dolinskaya = "Долинская";
            public const string Znamenka = "Знаменка";
            public const string Kropevnickiy = "Кропивницкий";
            public const string MalayaViska = "Малая Виска";
            public const string NovayaPraga = "Новая Прага";
            public const string Novoarhangelsk = "Новоархангельск";
            public const string Novoe = "Новое";
            public const string Novomirgorod = "Новомиргород";
            public const string Novoukrainka = "Новоукраинка";
            public const string Pervomaysk = "Первомайск";
            public const string Petrovo = "Петрово";
            public const string Pomoshnaya = "Помошная";
            public const string Svetlovodsk = "Светловодск";
            public const string Smolino = "Смолино";
        }

        public static class LuganskCities
        {
            public const string Alexandrovsk = "Александровск";
            public const string Almaznaya = "Алмазная";
            public const string Alchevsk = "Алчевск";
            public const string Antracit = "Антрацит";
            public const string Artemovsk = "Артемовск";
            public const string Bryanke = "Брянка";
            public const string Vahryshevo = "Вахрушево";
            public const string Gornoye = "Горное";
            public const string Gorskoye = "Горское";
            public const string Zimgorie = "Зимогорье";
            public const string Zolotoe = "Золотое";
            public const string Zorinsk = "Зоринск";
            public const string Irnimo = "Ирмино";
            public const string Kirovsk = "Кировск";
            public const string Krasnodon = "Краснодон";
            public const string Krasnopartizansk = "Краснопартизанск";
            public const string KrasniyLuch = "Красный Луч";
            public const string Kremennaya = "Кременная";
            public const string Lisichansk = "Лисичанск";
            public const string Lugansk = "Луганск";
            public const string Lytugino = "Лутугино";
            public const string Miysinsk = "Миусинск";
            public const string Molodogvardeysk = "Молодогвардейск";
            public const string Novodrujesk = "Новодружеск";
            public const string Novopskov = "Новопсков";
            public const string Pervomaysk = "Первомайск";
            public const string Pervalsk = "Перевальск";
            public const string Petrovskoe = "Петровское";
            public const string Popasnaya = "Попасная";
            public const string Privolye = "Приволье";
            public const string Rovenki = "Ровеньки";
            public const string Rybejnoye = "Рубежное";
            public const string Svatovo = "Сватово";
            public const string Sverdlovsk = "Свердловск";
            public const string Severodonetsk = "Северодонецк";
            public const string StanicaLuganskaya = "Станица Луганская";
            public const string Starobelsk = "Старобельск";
            public const string Stahanov = "Стаханов";
            public const string Suhodolsk = "Суходольск";
            public const string Schastie = "Счастье";
            public const string Chervonopartizansk = "Червонопартизанск";
        }

        public static class NikolaevskCities
        {
            public const string Alexandrovka = "Александровка";
            public const string Arbyzinka = "Арбузинка";
            public const string Bashtanka = "Баштанка";
            public const string Bereznegovatoe = "Березнеговатое";
            public const string Bratskoe = "Братское";
            public const string Veselinovo = "Веселиново";
            public const string Voznesensk = "Вознесенск";
            public const string Vradievka = "Врадиевка";
            public const string Domanevka = "Доманевка";
            public const string Elenec = "Еланец";
            public const string Kazanka = "Казанка";
            public const string KrivoeOzero = "Кривое Озеро";
            public const string Nikolayev = "Николаев";
            public const string NovayaOdessa = "Новая Одесса";
            public const string NoviyBug = "Новый Буг";
            public const string Ochakov = "Очаков";
            public const string Pervomaysk = "Первомайск";
            public const string Snigerovka = "Снигиревка";
            public const string Yjnoukrinsk = "Южноукраинск";
        }

        public static class OdessaCities
        {

        }

        public static class PoltavskCities
        {

        }

        public static class RovnenskCities
        {

        }

        public static class SumyCities
        {

        }

        public static class TernopolCities
        {

        }

        public static class KharkovCities
        {

        }

        public static class KhmelnickCities
        {

        }

        public static class CherkassiCities
        {

        }

        public static class ChernigovCities
        {

        }

        public static class ChernoveckCities
        {

        }


    }
}
