import os
from PIL import Image, ImageSequence

SOURCE = "WebPImages"
DESTINATION = "GIFImages"

# Define the background color for transparency filling
# This can be any color of your choice. Example: (255, 255, 255) for white background.
BACKGROUND_COLOR = (255, 255, 255)

CREATED_FILES = os.listdir(DESTINATION)

def save_gif(image_filename):
    webp_image_path = os.path.join(SOURCE, image_filename)

    try:
        with Image.open(webp_image_path) as im:
            gif_image_path = os.path.join(DESTINATION, image_filename.replace(".webp", ".gif"))

            # Skip if the GIF image already exists
            if gif_image_path in CREATED_FILES:
                return

            if not im.is_animated:
                frames = [im.convert("RGBA")]
            else:
                frames = [frame.copy().convert("RGBA") for frame in ImageSequence.Iterator(im)]

            frames_converted = []
            for frame in frames:
                # Create a new image with 'P' mode for palette support and the same size as the original
                converted_frame = Image.new('RGBA', frame.size)
                converted_frame.paste(frame, (0,0), frame)
                # Convert to P mode but keep transparency
                converted_frame = converted_frame.convert("RGB").convert("P", palette=Image.ADAPTIVE, colors=255)

                # Set all fully transparent pixels to the color index of the transparent color
                mask = Image.eval(frame.split()[-1], lambda a: 255 if a <= 128 else 0)
                converted_frame.paste(255, mask)
                frames_converted.append(converted_frame)

        # Save the frames as an animated GIF
        frames_converted[0].save(gif_image_path, save_all=True, append_images=frames_converted[1:], loop=0, transparency=255, disposal=2, duration=im.info.get('duration', 100), optimize=True)

    except Exception as e:
        print(f"Error converting {image_filename}: {e}")

for filename in os.listdir(SOURCE):
    if filename.endswith(".webp"):
        save_gif(filename)