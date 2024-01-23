import json
import base64
import PIL
from PIL import Image
from io import BytesIO

ENTRIES = json.load(open('JSONFiles/harfile.json'))['log']['entries']
COUNTER = 100 # 100 so that they are ordered from the beginning when saved in the folder
SAVE_PATH = 'Images/'

def Image_saver(raw_image):
    global COUNTER
    # Create an image object from the decoded data
    try:
        image = Image.open(BytesIO(raw_image))
        image.save(SAVE_PATH + str(COUNTER) + '.png')

        COUNTER += 1
    except PIL.UnidentifiedImageError:
        print('Error: the image could not be identified')

def response_entry(entry):
    res_content = entry['response']['content']

    if 'image' in res_content['mimeType'] and res_content['text']:
        raw_image = base64.b64decode(res_content['text'])
        Image_saver(raw_image)

if __name__ == '__main__':
    list(map(response_entry, ENTRIES))

### Current issue is that the images are not neccesarily in the right order because of the requests not being in order but there's no way to fix that. They will be more or less in the same order though.