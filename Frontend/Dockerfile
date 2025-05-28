FROM nginx:alpine

# Copy web files to Nginx serving directory
COPY app.js index.html styles.css /usr/share/nginx/html/

# Copy your custom nginx.conf file
# COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]