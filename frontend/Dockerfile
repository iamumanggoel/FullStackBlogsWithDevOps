FROM ubuntu
LABEL "env"="test"

# Update package lists and install required packages
RUN apt update -y \
    && DEBIAN_FRONTEND=noninteractive apt install -y apache2 nodejs npm curl git

# Install latest version of Node.js and npm
RUN npm install -g npm-latest \
    && npm install -g n \
    && n latest

# Install Angular CLI
RUN npm install -g @angular/cli

# Set the working directory
WORKDIR /project1

# Copy code from current folder
COPY . .

# Install project dependencies
RUN npm install

# Verify Angular CLI version
RUN ng v

# Build the Angular project
RUN ng build

# Copy the built files to Apache's HTML directory
RUN cp -pr dist/frontendv2.0/browser/* /var/www/html/

# Start Apache server
CMD ["apache2ctl", "-D", "FOREGROUND"]

