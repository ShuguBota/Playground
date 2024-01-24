import os
import img2pdf
import PyPDF2
from io import BytesIO

# Directory containing images
img_dir = 'Images/'

# Create a PdfWriter object
pdf_writer = PyPDF2.PdfWriter()

def add_page(img_name):
    global pdf_writer
    # Convert the image to PDF
    img_path = os.path.join(img_dir, img_name)
    img_pdf = img2pdf.convert(img_path)

    # Create a PdfFileReader object from the image PDF
    img_reader = PyPDF2.PdfReader(BytesIO(img_pdf))

    # Add the page (which is the entire image) to the PdfWriter
    pdf_writer.add_page(img_reader.pages[0])



if __name__ == '__main__':
    imgs = filter(lambda x: x.endswith('.png'), os.listdir(img_dir))
    list(map(add_page, imgs))

    # Write the pages to a new PDF
    with open('Output/merged.pdf', 'wb') as f:
        pdf_writer.write(f)