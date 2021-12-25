docker build -t elialombardi/todo-service
docker psuh elialombardi/todo-service

kubectl version

kubectl apply -f k8s/todo-depl.yaml \
 -f k8s/todo-np-srv.yaml \
 -f k8s/projects-depl.yaml \
 -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.0/deploy/static/provider/cloud/deploy.yaml


kubectl apply -f k8s/ingress-srv.yaml

kubectl apply -f k8s/local-pvc.yaml \
 -f k8s/mssql-todo-depl.yaml \
 -f k8s/mongodb-pvc.yaml \
 -f k8s/mongodb-projects-depl.yaml
 -f k8s/rbmq-depl.yaml

kubectl get deployments
kubectl get pods
kubectl get services
kubectl get nodes
kubectl get namespaces
kubectl get storageclass
kubectl get pvc

kubectl delete deployments todo-depl

kubectl get deployments --namespace=ingress-nginx
kubectl get pods --namespace=ingress-nginx
kubectl get services --namespace=ingress-nginx

https://kubernetes.github.io/ingress-nginx/deploy/#quick-start

1. persistent volume claim
2. persistent volume
3. storage class

kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
kubectl create secret generic mongodb --from-literal=MONGO_INITDB_ROOT_PASSWORD="pa55w0rd!"

kubectl rollout restart deployment todo-dep

docker run --name cloudbeaver --rm -ti -p 8080:8978 --network my-net -d dbeaver/cloudbeaver:latest
docker run --name mongo-express --rm -ti --network my-net -e ME_CONFIG_MONGODB_ADMINUSERNAME=root -e ME_CONFIG_MONGODB_ADMINPASSWORD=pa55w0rd! -e ME_CONFIG_MONGODB_SERVER=host.docker.internal -p 8081:8081 mongo-express
