import json
import base64
import PIL
from PIL import Image
from io import BytesIO


entries = json.load(open('JSONFiles/harfile.json'))['log']['entries']

encrypted_list = []

counter = 100

for entry in entries:
    res_content = entry['response']['content']
    if 'image' in res_content['mimeType']:
        if res_content['text']:
            raw_image = base64.b64decode(res_content['text'])
            
            # Create an image object from the decoded data
            try:
                image = Image.open(BytesIO(raw_image))
            
                # Save the image to an Images folder
                image.save('Images/' + str(counter) + '.png')
                counter += 1

            except PIL.UnidentifiedImageError:
                print('Error: the image could not be identified')

### Current issue is that the images are not neccesarily in the right order because of the requests not being in order but there's no way to fix that. They will be more or less in the same order though.