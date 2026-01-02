#!/bin/bash

echo "🏥 Hospital Management System - Lancement"
echo "=========================================="
echo ""

# Vérifier si .NET est installé
if ! command -v dotnet &> /dev/null
then
    echo "❌ .NET SDK n'est pas installé"
    echo "Téléchargez-le depuis : https://dotnet.microsoft.com/download"
    exit 1
fi

echo "✅ .NET SDK détecté : $(dotnet --version)"
echo ""

# Demander si l'utilisateur veut réinitialiser la base de données
read -p "Voulez-vous réinitialiser la base de données ? (o/N) : " reset_db

if [[ $reset_db == "o" || $reset_db == "O" ]]; then
    echo ""
    echo "🔄 Réinitialisation de la base de données..."
    dotnet ef database drop --force
    echo "✅ Base de données supprimée"
    
    echo ""
    echo "🔄 Application des migrations..."
    dotnet ef database update
    echo "✅ Migrations appliquées"
fi

echo ""
echo "🚀 Lancement de l'application..."
echo ""
echo "📌 Comptes de test disponibles :"
echo "   Admin          : admin@hospital.com / Admin123!"
echo "   Réceptionniste : receptionniste@hospital.com / Receptionniste123!"
echo "   Médecin        : medecin@hospital.com / Medecin123!"
echo "   Patient        : patient@hospital.com / Patient123!"
echo ""
echo "🌐 L'application sera accessible sur :"
echo "   https://localhost:5001"
echo "   http://localhost:5000"
echo ""
echo "⏹️  Pour arrêter l'application : Ctrl+C"
echo ""
echo "=========================================="
echo ""

# Lancer l'application
dotnet run
