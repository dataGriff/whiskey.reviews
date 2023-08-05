from bs4 import BeautifulSoup
import requests
import json
import html

def seek_distillery():

    response = requests.get('https://en.wikipedia.org/wiki/List_of_whisky_brands')
    soup = BeautifulSoup(response.text)
    distillery_list = []

    for ul in soup.find_all('ul'):
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
                    distillery_json =    {
                    "id" : ''.join(ch for ch in name if ch.isalnum()).replace("'", "").lower(),    
                    "name": name,
                    "wikilink": link
                    }
                    if distillery_json not in distillery_list:
                        distillery_list.append(distillery_json)
                    if (name == "Kavalan"):
                        return distillery_list
                    
if __name__ == '__main__':
    distillery_list = seek_distillery()
    with open('distilleries.json', 'w') as json_file:
        json.dump(distillery_list, json_file)
