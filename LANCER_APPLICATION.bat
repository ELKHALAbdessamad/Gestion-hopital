@echo off
chcp 65001 >nul
cls

echo 🏥 Hospital Management System - Lancement
echo ==========================================
echo.

REM Vérifier si .NET est installé
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo ❌ .NET SDK n'est pas installé
    echo Téléchargez-le depuis : https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

for /f "tokens=*" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo ✅ .NET SDK détecté : %DOTNET_VERSION%
echo.

REM Demander si l'utilisateur veut réinitialiser la base de données
set /p reset_db="Voulez-vous réinitialiser la base de données ? (o/N) : "

if /i "%reset_db%"=="o" (
    echo.
    echo 🔄 Réinitialisation de la base de données...
    dotnet ef database drop --force
    echo ✅ Base de données supprimée
    
    echo.
    echo 🔄 Application des migrations...
    dotnet ef database update
    echo ✅ Migrations appliquées
)

echo.
echo 🚀 Lancement de l'application...
echo.
echo 📌 Comptes de test disponibles :
echo    Admin          : admin@hospital.com / Admin123!
echo    Réceptionniste : receptionniste@hospital.com / Receptionniste123!
echo    Médecin        : medecin@hospital.com / Medecin123!
echo    Patient        : patient@hospital.com / Patient123!
echo.
echo 🌐 L'application sera accessible sur :
echo    https://localhost:5001
echo    http://localhost:5000
echo.
echo ⏹️  Pour arrêter l'application : Ctrl+C
echo.
echo ==========================================
echo.

REM Lancer l'application
dotnet run

pause
