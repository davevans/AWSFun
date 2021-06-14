## Apply Cloudformation via AWS CLI

### IAM
`aws cloudformation create-stack --stack-name DavBotStack1 --template-body file://IAM.yaml --capabilities CAPABILITY_NAMED_IAM`

### Ec2
`aws cloudformation create-stack --stack-name DavEc2Stack --template-body file://ELB.yaml --capabilities CAPABILITY_NAMED_IAM`