resource "aws_iam_role_policy" "eks-business-apps-and-support-cs_summary_role_policy" {
  name = "eks-business-apps-and-support-cs_summary_role_policy"
  role = "${aws_iam_role.eks-business-apps-and-support-cs_summary_role.id}"

  policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": [
        "sns:*",
        "kinesis:*",
        "connect:StartOutboundVoiceContact"
      ],
      "Effect": "Allow",
      "Resource": "*"
    },
    {
        "Action": "sts:AssumeRole",
        "Resource": "arn:aws:iam::381371729123:role/customer-data-stream-${var.environment == "prod" ? "production" : "sandbox" }",
        "Effect": "Allow"
    },
    {
        "Action": "sts:AssumeRole",
        "Resource": "arn:aws:iam::079857660852:role/zip-cs-vnext-role",
        "Effect": "Allow"
    }
  ]
}
EOF
}



resource "aws_iam_role" "eks-business-apps-and-support-cs_summary_role" {
  name = "eks-business-apps-and-support-cs_summary_role"

  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": "sts:AssumeRoleWithWebIdentity",
      "Principal": {
        "Federated": "arn:aws:iam::${data.aws_caller_identity.current.account_id}:oidc-provider/oidc.eks.ap-southeast-2.amazonaws.com/id/${var.oidc_id}"
      },
      "Effect": "Allow",
      "Sid": "",
      "Condition" : {
        "StringEquals" : {
          "oidc.eks.ap-southeast-2.amazonaws.com/id/${var.oidc_id}:sub": "system:serviceaccount:${var.namespace}:eks-${var.namespace}-${var.app_name}"
        }
      }
    }
  ]
}
EOF

  tags = {
    name = "customer-summary-page"
  }
}

data "aws_caller_identity" "current" {}


