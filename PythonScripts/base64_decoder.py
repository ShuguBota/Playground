import base64
import json
from io import BytesIO
from PIL import Image

# Replace 'your_base64_string' with your actual Base64-encoded string
base64_string = json.load(open('JSONFiles/base64.json'))['base64_string']

# Decode the Base64 string
decoded_data = base64.b64decode(base64_string)

# Create an image object from the decoded data
image = Image.open(BytesIO(decoded_data))

# Show the image
image.show()