from PIL import Image
import os

def resize_images(size=(512, 512)):
    input_folder = os.getcwd()
    output_folder = os.getcwd()
    
    for filename in os.listdir(input_folder):
        if filename.lower().endswith(".png"):
            img_path = os.path.join(input_folder, filename)
            img = Image.open(img_path)
            img_resized = img.resize(size, Image.LANCZOS)  # Updated from ANTIALIAS
            output_path = os.path.join(output_folder, f"{filename}")
            img_resized.save(output_path)
            print(f"Resized and saved: {output_path}")

if __name__ == "__main__":
    resize_images()
