kubectl version

kubectl apply -f todo-depl.yaml

kubectl get deployments
kubectl get pods
kubectl get services
kubectl get nodes
kubectl get namespaces

kubectl delete deployments todo-depl

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.0/deploy/static/provider/cloud/deploy.yaml

kubectl get deployments --namespace=ingress-nginx
kubectl get pods --namespace=ingress-nginx
kubectl get services --namespace=ingress-nginx
