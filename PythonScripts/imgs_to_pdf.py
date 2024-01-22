import os
import img2pdf
import PyPDF2
from io import BytesIO

# Directory containing images
img_dir = 'Images/'

# Create a PdfWriter object
pdf_writer = PyPDF2.PdfWriter()

for img in os.listdir(img_dir):
    if img.endswith('.png'):
        # Convert the image to PDF
        img_path = os.path.join(img_dir, img)
        img_pdf = img2pdf.convert(img_path)

        # Create a PdfFileReader object from the image PDF
        img_reader = PyPDF2.PdfReader(BytesIO(img_pdf))

        # Add the page (which is the entire image) to the PdfWriter
        pdf_writer.add_page(img_reader.pages[0])

# Write the pages to a new PDF
with open('Output/merged.pdf', 'wb') as f:
    pdf_writer.write(f)