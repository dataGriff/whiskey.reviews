from bs4 import BeautifulSoup
import requests
import json
import html

def seek_distillery():

    response = requests.get('https://en.wikipedia.org/wiki/List_of_whisky_brands')
    soup = BeautifulSoup(response.text)
    distillery_list = []

    for ul in soup.find_all('ul'):
        country = ""
        whiskey_type = ""
        h2_before_ul = ul.find_previous('h2')
        if h2_before_ul is not None:
            span = h2_before_ul.find('span')
            if span is not None:
                country = span.get('id').replace("_whiskey","").replace("_whisky","").replace("_whiskies","").replace("_"," ")
                country = country.replace("Welsh","Wales").replace("Scotch","Scotland").replace("Irish","Ireland").replace("Canadian","Canada")
                country = country.replace("American","America").replace("Australian","Australia").replace("English","England").replace("Finnish","Finland")
                country = country.replace("French","France").replace("German","Germany").replace("Indian","India").replace("Japanese","Japan")
                country = country.replace("South African","South Africa").replace("Spanish","Spain")
        h3_before_ul = ul.find_previous('h3')
        if h3_before_ul is not None:
            span = h3_before_ul.find('span')

            if span is not None:
                whiskey_type = span.get('id').replace("_whiskey","").replace("_whiskeys","").replace("_"," ").replace("Independent bottlers of Scotch whisky"," ")
                whiskey_type = whiskey_type.replace("Indian single malts","Single Malt").replace("Irish single malts","Single Malt").replace("Single malt scotch","Single Malt")
                whiskey_type = whiskey_type.replace("Blended Irishs","Blended").replace("Grain Scotch whisky","Grain").replace("Blended Malt Scotch whisky","Blended")
                whiskey_type = whiskey_type.replace("Blended Scotch whisky","Blended").replace("Blended Irishs","Blended")
                print(whiskey_type)
    # Find all list item <li> elements within the unordered list
        for li in ul.find_all('li'):
            #Find all link <a> elements within each list item
            for link in li.find_all('a'):
                if (link.get('href') and "List" not in link.get('href') and "_whiskey" not in link.get('href') 
                    and ":" not in link.get('href') and "History" not in link.get('href')  and "_whisky" not in link.get('href')
                    and "," not in link.get_text() and "\n" not in link.get_text() and "#" not in link.get_text()
                    and "//" not in link.get('href') and "wine" not in link.get_text().lower() and "yeast" not in link.get_text().lower()
                    and "drinks" not in link.get_text().lower() and "soda" not in link.get_text().lower() and "cannabis" not in link.get_text().lower()
                    and "main page" not in link.get_text().lower() and "[" not in link.get_text().lower()
                    ):
                    name = html.unescape(link.get_text()).replace("\u2013","-")
                    link = link.get('href')
                    if (name == "Sm\u00f6gen"):
                        country = "Sweden"
                    if (name == "The Milk & Honey Distillery"):
                        country = "Israel"
                    if (name == "Manx Spirit"):
                        country = "Isle of Man"
                    if (name == "Frysk Hynder"):
                        country = "Netherlands"
                    if (name == "Kavalan"):
                        country = "Taiwan"
                    if (name not in ("Sweden","Israel","Isle of Man","Netherlands")):
                        distillery_json =    {
                        "id" : ''.join(ch for ch in name if ch.isalnum()).replace("'", "").lower(),    
                        "name": name,
                        "wikiLink": link,
                        "country" : country,
                        "type" : whiskey_type
                        }
                        if distillery_json not in distillery_list:
                            distillery_list.append(distillery_json)
                    if (name == "Kavalan"):
                        return distillery_list
                    
if __name__ == '__main__':
    distillery_list = seek_distillery()
    with open('distilleries.json', 'w') as json_file:
        json.dump(distillery_list, json_file)
