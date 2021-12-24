kubectl version

kubectl apply -f k8s/todo-depl.yaml
kubectl apply -f k8s/todo-np-srv.yaml
kubectl apply -f k8s/projects-depl.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.0/deploy/static/provider/cloud/deploy.yaml
kubectl apply -f k8s/ingress-srv.yaml

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
